using Bainah.Core.DTOs;
using Bainah.Core.Entities;

namespace Bainah.Core.Interfaces;

public interface INationalityRepository 
{
    Task<IEnumerable<NationalityDto>> GetAllAsyncDto();
}