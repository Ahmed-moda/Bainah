using AutoMapper;
using Bainah.Core.Entities;
using Bainah.Core.DTOs;
using Bainah.Core.Interfaces;
using Bainah.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;

namespace Bainah.Infrastructure.Repositories;

public class RegionRepository : GenericRepository<Region>, IRegionRepository
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _db;

    public RegionRepository(AppDbContext db, IMapper mapper) : base(db)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RegionDto>> GetAllAsyncDto()
    {
        var regions = await _db.Regions.AsNoTracking().ToListAsync();
        return _mapper.Map<IEnumerable<RegionDto>>(regions);
    }
}
