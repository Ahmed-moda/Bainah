using Bainah.Core.Entities;
using Bainah.Core.Interfaces;
using Bainah.CoreApi.Common;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bainah.CoreApi.Controllers;
[ApiController]
[Route("api/webpages")]
public class WebPagesController : ControllerBase
{
    private readonly IWebPageRepository _repo;

    public WebPagesController(IWebPageRepository repo)
    {
        _repo = repo;
    }


    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id) { 

        return Ok(await _repo.GetWebPageWithSectionsAsync(id));

    }


}
