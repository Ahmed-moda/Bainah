using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Bainah.Core.Interfaces;
using Bainah.Core.Entities;
using Bainah.CoreApi.Common;

namespace Bainah.CoreApi.Controllers;
[ApiController]
[Route("api/accounts")]
public class AccountsController : ControllerBase
{
    private readonly IGenericRepository<Account> _repo;
    public AccountsController(IGenericRepository<Account> repo) => _repo = repo;

    [HttpGet]
    public async Task<DataResponse<IEnumerable<Account>>> GetAll([FromQuery] int? webPageId, [FromQuery] int? noticeCategoryId)
    {
        // simple filtering demo: for PageSection support webPageId; for Notice support noticeCategoryId
        if (typeof(Account) == typeof(PageSection) && webPageId.HasValue)
        {
            var sections = await _repo.FindAsync(x => ((Bainah.Core.Entities.PageSection)(object)x).WebPageId == webPageId.Value);
            var ordered = sections.Cast<Bainah.Core.Entities.PageSection>().OrderBy(s => s.Order).Cast<Account>();
            return DataResponse<IEnumerable<Account>>.Ok(ordered);
        }
        if (typeof(Account) == typeof(Notice) && noticeCategoryId.HasValue)
        {
            var list = await _repo.FindAsync(x => ((Bainah.Core.Entities.Notice)(object)x).NoticeCategoryId == noticeCategoryId.Value);
            return DataResponse<IEnumerable<Account>>.Ok(list);
        }
        var all = await _repo.GetAllAsync();
        return DataResponse<IEnumerable<Account>>.Ok(all);
    }

    [HttpGet("{id:int}")]
    public async Task<DataResponse<Account>> Get(int id) { var item = await _repo.GetByIdAsync(id); return item==null?DataResponse<Account>.Fail("Not found"):DataResponse<Account>.Ok(item); }

    [HttpPost]
    [Authorize(Roles="Admin")]
    public async Task<DataResponse<Account>> Create([FromBody] Account model) { var created = await _repo.AddAsync(model); return DataResponse<Account>.Ok(created); }

    [HttpPut("{id:int}")]
    [Authorize(Roles="Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] Account model) { model.Id = id; await _repo.UpdateAsync(model); return NoContent(); }

    [HttpDelete("{id:int}")]
    [Authorize(Roles="Admin")]
    public async Task<IActionResult> Delete(int id) { await _repo.DeleteAsync(id); return NoContent(); }
}
