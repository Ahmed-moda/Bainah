using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Bainah.Core.Interfaces;
using Bainah.Core.Entities;
using Bainah.CoreApi.Common;

namespace Bainah.CoreApi.Controllers;
[ApiController]
[Route("api/inquirycategorys")]
public class InquiryCategorysController : ControllerBase
{
    private readonly IGenericRepository<InquiryCategory> _repo;
    public InquiryCategorysController(IGenericRepository<InquiryCategory> repo) => _repo = repo;

    [HttpGet]
    public async Task<DataResponse<IEnumerable<InquiryCategory>>> GetAll([FromQuery] int? webPageId, [FromQuery] int? noticeCategoryId)
    {
        // simple filtering demo: for PageSection support webPageId; for Notice support noticeCategoryId
        if (typeof(InquiryCategory) == typeof(PageSection) && webPageId.HasValue)
        {
            var sections = await _repo.FindAsync(x => ((Bainah.Core.Entities.PageSection)(object)x).WebPageId == webPageId.Value);
            var ordered = sections.Cast<Bainah.Core.Entities.PageSection>().OrderBy(s => s.Order).Cast<InquiryCategory>();
            return DataResponse<IEnumerable<InquiryCategory>>.Ok(ordered);
        }
        if (typeof(InquiryCategory) == typeof(Notice) && noticeCategoryId.HasValue)
        {
            var list = await _repo.FindAsync(x => ((Bainah.Core.Entities.Notice)(object)x).NoticeCategoryId == noticeCategoryId.Value);
            return DataResponse<IEnumerable<InquiryCategory>>.Ok(list);
        }
        var all = await _repo.GetAllAsync();
        return DataResponse<IEnumerable<InquiryCategory>>.Ok(all);
    }

    [HttpGet("{id:int}")]
    public async Task<DataResponse<InquiryCategory>> Get(int id) { var item = await _repo.GetByIdAsync(id); return item==null?DataResponse<InquiryCategory>.Fail("Not found"):DataResponse<InquiryCategory>.Ok(item); }

    [HttpPost]
    [Authorize(Roles="Admin")]
    public async Task<DataResponse<InquiryCategory>> Create([FromBody] InquiryCategory model) { var created = await _repo.AddAsync(model); return DataResponse<InquiryCategory>.Ok(created); }

    [HttpPut("{id:int}")]
    [Authorize(Roles="Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] InquiryCategory model) { model.Id = id; await _repo.UpdateAsync(model); return NoContent(); }

    [HttpDelete("{id:int}")]
    [Authorize(Roles="Admin")]
    public async Task<IActionResult> Delete(int id) { await _repo.DeleteAsync(id); return NoContent(); }
}
