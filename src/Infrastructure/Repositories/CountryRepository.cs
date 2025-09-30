using AutoMapper;
using Bainah.Core.Entities;
using Bainah.Core.DTOs;
using Bainah.Core.Interfaces;
using Bainah.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Bainah.Infrastructure.Repositories;

public class CountryRepository :ICountryRepository
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _db;

    public CountryRepository(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CountryDto>> GetAllAsyncDto()
    {
        var countries = await _db.Countries.AsNoTracking().ToListAsync();
        return _mapper.Map<IEnumerable<CountryDto>>(countries);
    }
}