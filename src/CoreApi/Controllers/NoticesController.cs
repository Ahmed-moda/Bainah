using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Bainah.Core.Interfaces;
using Bainah.Core.Entities;
using Bainah.CoreApi.Common;

namespace Bainah.CoreApi.Controllers;
[ApiController]
[Route("api/notices")]
public class NoticesController : ControllerBase
{
    private readonly IGenericRepository<Notice> _repo;
    public NoticesController(IGenericRepository<Notice> repo) => _repo = repo;

    [HttpGet]
    public async Task<DataResponse<IEnumerable<Notice>>> GetAll([FromQuery] int? webPageId, [FromQuery] int? noticeCategoryId)
    {
        // simple filtering demo: for PageSection support webPageId; for Notice support noticeCategoryId
        if (typeof(Notice) == typeof(PageSection) && webPageId.HasValue)
        {
            var sections = await _repo.FindAsync(x => ((Bainah.Core.Entities.PageSection)(object)x).WebPageId == webPageId.Value);
            var ordered = sections.Cast<Bainah.Core.Entities.PageSection>().OrderBy(s => s.Order).Cast<Notice>();
            return DataResponse<IEnumerable<Notice>>.Ok(ordered);
        }
        if (typeof(Notice) == typeof(Notice) && noticeCategoryId.HasValue)
        {
            var list = await _repo.FindAsync(x => ((Bainah.Core.Entities.Notice)(object)x).NoticeCategoryId == noticeCategoryId.Value);
            return DataResponse<IEnumerable<Notice>>.Ok(list);
        }
        var all = await _repo.GetAllAsync();
        return DataResponse<IEnumerable<Notice>>.Ok(all);
    }

    [HttpGet("{id:int}")]
    public async Task<DataResponse<Notice>> Get(int id) { var item = await _repo.GetByIdAsync(id); return item==null?DataResponse<Notice>.Fail("Not found"):DataResponse<Notice>.Ok(item); }

    [HttpPost]
    [Authorize(Roles="Admin")]
    public async Task<DataResponse<Notice>> Create([FromBody] Notice model) { var created = await _repo.AddAsync(model); return DataResponse<Notice>.Ok(created); }

    [HttpPut("{id:int}")]
    [Authorize(Roles="Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] Notice model) { model.Id = id; await _repo.UpdateAsync(model); return NoContent(); }

    [HttpDelete("{id:int}")]
    [Authorize(Roles="Admin")]
    public async Task<IActionResult> Delete(int id) { await _repo.DeleteAsync(id); return NoContent(); }
}
