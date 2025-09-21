using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Bainah.Core.Interfaces;
using Bainah.Core.Entities;
using Bainah.CoreApi.Common;

namespace Bainah.CoreApi.Controllers;
[ApiController]
[Route("api/noticecategorys")]
public class NoticeCategorysController : ControllerBase
{
    private readonly IGenericRepository<NoticeCategory> _repo;
    public NoticeCategorysController(IGenericRepository<NoticeCategory> repo) => _repo = repo;

    [HttpGet]
    public async Task<DataResponse<IEnumerable<NoticeCategory>>> GetAll([FromQuery] int? webPageId, [FromQuery] int? noticeCategoryId)
    {
        // simple filtering demo: for PageSection support webPageId; for Notice support noticeCategoryId
        if (typeof(NoticeCategory) == typeof(PageSection) && webPageId.HasValue)
        {
            var sections = await _repo.FindAsync(x => ((Bainah.Core.Entities.PageSection)(object)x).WebPageId == webPageId.Value);
            var ordered = sections.Cast<Bainah.Core.Entities.PageSection>().OrderBy(s => s.Order).Cast<NoticeCategory>();
            return DataResponse<IEnumerable<NoticeCategory>>.Ok(ordered);
        }
        if (typeof(NoticeCategory) == typeof(Notice) && noticeCategoryId.HasValue)
        {
            var list = await _repo.FindAsync(x => ((Bainah.Core.Entities.Notice)(object)x).NoticeCategoryId == noticeCategoryId.Value);
            return DataResponse<IEnumerable<NoticeCategory>>.Ok(list);
        }
        var all = await _repo.GetAllAsync();
        return DataResponse<IEnumerable<NoticeCategory>>.Ok(all);
    }

    [HttpGet("{id:int}")]
    public async Task<DataResponse<NoticeCategory>> Get(int id) { var item = await _repo.GetByIdAsync(id); return item==null?DataResponse<NoticeCategory>.Fail("Not found"):DataResponse<NoticeCategory>.Ok(item); }

    [HttpPost]
    [Authorize(Roles="Admin")]
    public async Task<DataResponse<NoticeCategory>> Create([FromBody] NoticeCategory model) { var created = await _repo.AddAsync(model); return DataResponse<NoticeCategory>.Ok(created); }

    [HttpPut("{id:int}")]
    [Authorize(Roles="Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] NoticeCategory model) { model.Id = id; await _repo.UpdateAsync(model); return NoContent(); }

    [HttpDelete("{id:int}")]
    [Authorize(Roles="Admin")]
    public async Task<IActionResult> Delete(int id) { await _repo.DeleteAsync(id); return NoContent(); }
}
