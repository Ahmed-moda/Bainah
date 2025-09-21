using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
namespace Bainah.AdminBFF.Controllers;
[ApiController]
[Route("api/admin/noticecategorys")]
[Authorize(Policy = "AdminOnly")]
public class NoticeCategorysController : ControllerBase
{
    private readonly IHttpClientFactory _http;
    public NoticeCategorysController(IHttpClientFactory http) => _http = http;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var client = _http.CreateClient("CoreApi");
        var resp = await client.GetFromJsonAsync<object>($"api/noticecategorys");
        return Ok(resp);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] object dto)
    {
        var client = _http.CreateClient("CoreApi");
        var resp = await client.PostAsJsonAsync($"api/noticecategorys", dto);
        return StatusCode((int)resp.StatusCode, await resp.Content.ReadAsStringAsync());
    }
}
