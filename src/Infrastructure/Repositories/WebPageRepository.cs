using Bainah.Core.DTOs.Pages;
using Bainah.Core.Entities;
using Bainah.CoreApi.Common;
using Bainah.Infrastructure.Persistence;
using Core.DTOs.Pages;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class WebPageRepository : IWebPageRepository
    {
        private readonly AppDbContext _context;
        public WebPageRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<DataResponse<WebPageDto?>> GetWebPageWithSectionsAsync(int id)
        {
            var result = new DataResponse<WebPageDto?>();
            try
            {
                var page =await _context.WebPages.Where(z => z.Id == id).Include(x => x.Sections).ThenInclude(x => x.Photos).FirstOrDefaultAsync();
                if (page == null)
                {
                    result.Success = false;
                    result.Message = "Page not found";
                    result.Data = null;
                    return result;
                }

                var dto = new WebPageDto
                {
                    Id = page.Id,
                    Name = page.Name,
                    Slug = page.Slug,
                    Sections = page.Sections
                        .OrderBy(s => s.Order)
                        .Select(s => new PageSectionDto
                        {
                            Id = s.Id,
                            Header = s.Header,
                            HeaderAr = s.HeaderAr,
                            Content = s.Content,
                            ContentAr = s.ContentAr,
                            Order = s.Order,
                            Photos = s.Photos
                                .Select(p => new PageSectionPhotoDto
                                {
                                    FileName = p.FileName,
                                    FilePath = p.FilePath
                                }).ToList()
                        }).ToList()
                };
                result.Success = true;
                result.Message = "done";
                result.Data = dto;
                return result;
            }
            catch (Exception ex) { 
            
                result.Success = false;
                result.Message = ex.Message;
                result.Data = null;
                return result;
            }
            
        }
    }
    
}
