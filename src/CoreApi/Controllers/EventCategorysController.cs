using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Bainah.Core.Interfaces;
using Bainah.Core.Entities;
using Bainah.CoreApi.Common;

namespace Bainah.CoreApi.Controllers;
[ApiController]
[Route("api/eventcategorys")]
public class EventCategorysController : ControllerBase
{
    private readonly IGenericRepository<EventCategory> _repo;
    public EventCategorysController(IGenericRepository<EventCategory> repo) => _repo = repo;

    [HttpGet]
    public async Task<DataResponse<IEnumerable<EventCategory>>> GetAll([FromQuery] int? webPageId, [FromQuery] int? noticeCategoryId)
    {
        // simple filtering demo: for PageSection support webPageId; for Notice support noticeCategoryId
        if (typeof(EventCategory) == typeof(PageSection) && webPageId.HasValue)
        {
            var sections = await _repo.FindAsync(x => ((Bainah.Core.Entities.PageSection)(object)x).WebPageId == webPageId.Value);
            var ordered = sections.Cast<Bainah.Core.Entities.PageSection>().OrderBy(s => s.Order).Cast<EventCategory>();
            return DataResponse<IEnumerable<EventCategory>>.Ok(ordered);
        }
        if (typeof(EventCategory) == typeof(Notice) && noticeCategoryId.HasValue)
        {
            var list = await _repo.FindAsync(x => ((Bainah.Core.Entities.Notice)(object)x).NoticeCategoryId == noticeCategoryId.Value);
            return DataResponse<IEnumerable<EventCategory>>.Ok(list);
        }
        var all = await _repo.GetAllAsync();
        return DataResponse<IEnumerable<EventCategory>>.Ok(all);
    }

    [HttpGet("{id:int}")]
    public async Task<DataResponse<EventCategory>> Get(int id) { var item = await _repo.GetByIdAsync(id); return item==null?DataResponse<EventCategory>.Fail("Not found"):DataResponse<EventCategory>.Ok(item); }

    [HttpPost]
    [Authorize(Roles="Admin")]
    public async Task<DataResponse<EventCategory>> Create([FromBody] EventCategory model) { var created = await _repo.AddAsync(model); return DataResponse<EventCategory>.Ok(created); }

    [HttpPut("{id:int}")]
    [Authorize(Roles="Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] EventCategory model) { model.Id = id; await _repo.UpdateAsync(model); return NoContent(); }

    [HttpDelete("{id:int}")]
    [Authorize(Roles="Admin")]
    public async Task<IActionResult> Delete(int id) { await _repo.DeleteAsync(id); return NoContent(); }
}
