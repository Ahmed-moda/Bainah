using Bainah.Core.Entities;
using Bainah.CoreApi.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
namespace Bainah.PublicBFF.Controllers;
[ApiController]
[Route("api/public/pagesections")]
public class PageSectionsController : ControllerBase
{
    private readonly IHttpClientFactory _http;
    public PageSectionsController(IHttpClientFactory http) => _http = http;

    [HttpGet("{webPageId:int}")]
    public async Task<IActionResult> GetByPage(int webPageId)
    {
        var client = _http.CreateClient("CoreApi");
        var resp = await client.GetFromJsonAsync<DataResponse<IEnumerable<PageSection>>>($"api/pagesections?webPageId={webPageId}");
        if (resp == null) return NoContent();
        var ordered = resp.Data?.OrderBy(s => s.Order)?.ToList(); // Convert to List to resolve the issue
        return Ok(DataResponse<IEnumerable<PageSection>>.Ok(ordered ?? new List<PageSection>()));
    }
}
