using AutoMapper;
using Bainah.Core.Entities;
using Bainah.Core.DTOs;
using Bainah.Core.Interfaces;
using Bainah.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;

namespace Bainah.Infrastructure.Repositories;

public class CityRepository : ICityRepository
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _db;

    public CityRepository(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CityDto>> GetAllAsyncDto()
    {
        var cities = await _db.Cities.AsNoTracking().ToListAsync();
        return _mapper.Map<IEnumerable<CityDto>>(cities);
    }
}
