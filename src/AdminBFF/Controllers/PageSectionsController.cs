using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
namespace Bainah.AdminBFF.Controllers;
[ApiController]
[Route("api/admin/pagesections")]
[Authorize(Policy = "AdminOnly")]
public class PageSectionsController : ControllerBase
{
    private readonly IHttpClientFactory _http;
    public PageSectionsController(IHttpClientFactory http) => _http = http;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var client = _http.CreateClient("CoreApi");
        var resp = await client.GetFromJsonAsync<object>($"api/pagesections");
        return Ok(resp);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] object dto)
    {
        var client = _http.CreateClient("CoreApi");
        var resp = await client.PostAsJsonAsync($"api/pagesections", dto);
        return StatusCode((int)resp.StatusCode, await resp.Content.ReadAsStringAsync());
    }
}
