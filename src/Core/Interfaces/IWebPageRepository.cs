using Bainah.Core.Entities;
using Bainah.CoreApi.Common;
using Core.DTOs.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IWebPageRepository
    {
        Task<DataResponse<WebPageDto?>> GetWebPageWithSectionsAsync(int id);

    }
}
