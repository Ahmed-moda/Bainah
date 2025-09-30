using Microsoft.AspNetCore.Mvc;
using Bainah.Core.Interfaces;
using Bainah.Core.DTOs;

namespace Bainah.CoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NationalitiesController : ControllerBase
{
    private readonly INationalityRepository _nationalityRepository;

    public NationalitiesController(INationalityRepository nationalityRepository)
    {
        _nationalityRepository = nationalityRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<NationalityDto>>> GetAll()
    {
        var nationalities = await _nationalityRepository.GetAllAsyncDto();
        return Ok(nationalities);
    }
}