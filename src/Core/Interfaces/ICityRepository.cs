using Bainah.Core.DTOs;
using Bainah.Core.Entities;
using Bainah.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICityRepository
    {
        Task<IEnumerable<CityDto>> GetAllAsyncDto();

    }
}
