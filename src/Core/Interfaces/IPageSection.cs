using Bainah.Core.Entities;
using Bainah.Core.Interfaces;
using Bainah.CoreApi.Common;
using CoreApi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPageSection
    {
        Task<IEnumerable<PageSection>> GetByWebPageIdAsync(int webPageId);
        Task<DataResponse<bool>> UpdatePageSectionAsync(PageSectionUpdateDto dto);

    }
}
