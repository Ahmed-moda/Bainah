using Microsoft.AspNetCore.Mvc;
using Bainah.Core.Interfaces;
using Bainah.Core.DTOs;
using Core.Interfaces;

namespace Bainah.CoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegionsController : ControllerBase
{
    private readonly IRegionRepository _regionRepository;

    public RegionsController(IRegionRepository regionRepository)
    {
        _regionRepository = regionRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RegionDto>>> GetAll()
    {
        var regions = await _regionRepository.GetAllAsyncDto();
        return Ok(regions);
    }
}