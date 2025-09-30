using Microsoft.AspNetCore.Mvc;
using Bainah.Core.Interfaces;
using Bainah.Core.DTOs;

namespace Bainah.CoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController : ControllerBase
{
    private readonly ICountryRepository _countryRepository;

    public CountriesController(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CountryDto>>> GetAll()
    {
        var countries = await _countryRepository.GetAllAsyncDto();
        return Ok(countries);
    }
}