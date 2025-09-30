using AutoMapper;
using Bainah.Core.Entities;
using Bainah.Core.DTOs;
using Bainah.Core.Interfaces;
using Bainah.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Bainah.Infrastructure.Repositories;

public class NationalityRepository : INationalityRepository
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _db;

    public NationalityRepository(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IEnumerable<NationalityDto>> GetAllAsyncDto()
    {
        var nationalities = await _db.Nationalities.AsNoTracking().ToListAsync();
        return _mapper.Map<IEnumerable<NationalityDto>>(nationalities);
    }
}