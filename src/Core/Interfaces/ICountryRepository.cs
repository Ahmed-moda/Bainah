using Bainah.Core.DTOs;
using Bainah.Core.Entities;

namespace Bainah.Core.Interfaces;

public interface ICountryRepository 
{
    Task<IEnumerable<CountryDto>> GetAllAsyncDto();
}