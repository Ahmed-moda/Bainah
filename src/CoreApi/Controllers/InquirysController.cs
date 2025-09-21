using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Bainah.Core.Interfaces;
using Bainah.Core.Entities;
using Bainah.CoreApi.Common;

namespace Bainah.CoreApi.Controllers;
[ApiController]
[Route("api/inquirys")]
public class InquirysController : ControllerBase
{
    private readonly IGenericRepository<Inquiry> _repo;
    public InquirysController(IGenericRepository<Inquiry> repo) => _repo = repo;

    [HttpGet]
    public async Task<DataResponse<IEnumerable<Inquiry>>> GetAll([FromQuery] int? webPageId, [FromQuery] int? noticeCategoryId)
    {
        // simple filtering demo: for PageSection support webPageId; for Notice support noticeCategoryId
        if (typeof(Inquiry) == typeof(PageSection) && webPageId.HasValue)
        {
            var sections = await _repo.FindAsync(x => ((Bainah.Core.Entities.PageSection)(object)x).WebPageId == webPageId.Value);
            var ordered = sections.Cast<Bainah.Core.Entities.PageSection>().OrderBy(s => s.Order).Cast<Inquiry>();
            return DataResponse<IEnumerable<Inquiry>>.Ok(ordered);
        }
        if (typeof(Inquiry) == typeof(Notice) && noticeCategoryId.HasValue)
        {
            var list = await _repo.FindAsync(x => ((Bainah.Core.Entities.Notice)(object)x).NoticeCategoryId == noticeCategoryId.Value);
            return DataResponse<IEnumerable<Inquiry>>.Ok(list);
        }
        var all = await _repo.GetAllAsync();
        return DataResponse<IEnumerable<Inquiry>>.Ok(all);
    }

    [HttpGet("{id:int}")]
    public async Task<DataResponse<Inquiry>> Get(int id) { var item = await _repo.GetByIdAsync(id); return item==null?DataResponse<Inquiry>.Fail("Not found"):DataResponse<Inquiry>.Ok(item); }

    [HttpPost]
    [Authorize(Roles="Admin")]
    public async Task<DataResponse<Inquiry>> Create([FromBody] Inquiry model) { var created = await _repo.AddAsync(model); return DataResponse<Inquiry>.Ok(created); }

    [HttpPut("{id:int}")]
    [Authorize(Roles="Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] Inquiry model) { model.Id = id; await _repo.UpdateAsync(model); return NoContent(); }

    [HttpDelete("{id:int}")]
    [Authorize(Roles="Admin")]
    public async Task<IActionResult> Delete(int id) { await _repo.DeleteAsync(id); return NoContent(); }
}
