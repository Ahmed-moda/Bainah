using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
namespace Bainah.PublicBFF.Controllers;
[ApiController]
[Route("api/public/accounts")]
public class AccountsController : ControllerBase
{
    private readonly IHttpClientFactory _http;
    public AccountsController(IHttpClientFactory http) => _http = http;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var client = _http.CreateClient("CoreApi");
        var resp = await client.GetFromJsonAsync<object>($"api/accounts");
        return Ok(resp);
    }
}
