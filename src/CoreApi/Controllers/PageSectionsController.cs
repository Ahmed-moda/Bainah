using Bainah.Core.Entities;
using Bainah.Core.Interfaces;
using Bainah.CoreApi.Common;
using Core.DTOs.Pages;
using Core.Interfaces;
using CoreApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bainah.CoreApi.Controllers;
[ApiController]
[Route("api/pagesections")]
public class PageSectionsController : ControllerBase
{
    private readonly IPageSection _repo;
    public PageSectionsController(IPageSection repo) => _repo = repo; 

    [HttpPut]
    //[Authorize(Roles="Admin")]
    public async Task<IActionResult> Update([FromForm] PageSectionUpdateDto model) {
        var result = await _repo.UpdatePageSectionAsync(model);
        return Ok(result);
    }

   
}
