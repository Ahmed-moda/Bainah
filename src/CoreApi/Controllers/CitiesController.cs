using Microsoft.AspNetCore.Mvc;
using Bainah.Core.Interfaces;
using Bainah.Core.DTOs;
using Core.Interfaces;

namespace Bainah.CoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitiesController : ControllerBase
{
    private readonly ICityRepository _cityRepository;

    public CitiesController(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CityDto>>> GetAll()
    {
        var cities = await _cityRepository.GetAllAsyncDto();
        return Ok(cities);
    }
}