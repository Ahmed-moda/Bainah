using Bainah.Core.Entities;
using Bainah.CoreApi.Common;
using Bainah.Infrastructure.Persistence;
using Core.Entities;
using Core.Interfaces;
using CoreApi.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PageSectionRepository : IPageSection
    {
        private readonly AppDbContext _context;
        private readonly string _uploadsFolder;

        public PageSectionRepository(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(),
            config["FileStorage:Sections"] ?? "wwwroot/uploads/sections");

            if (!Directory.Exists(_uploadsFolder))
                Directory.CreateDirectory(_uploadsFolder);
        }
        public Task<IEnumerable<PageSection>> GetByWebPageIdAsync(int webPageId)
        {
            throw new NotImplementedException();
        }
        public async Task<DataResponse<bool>> UpdatePageSectionAsync(PageSectionUpdateDto dto)
        {
            var result = new DataResponse<bool>();
            try
            {
                var section = await _context.PageSections
                .Include(s => s.Photos)
                .FirstOrDefaultAsync(s => s.Id == dto.Id);

                if (section == null) {
                    result.Success = false;
                    result.Message = "not Found";
                    result.Data = false;
                }

                // update text fields
                section.Header = dto.Header;
                section.HeaderAr = dto.HeaderAr;
                section.Content = dto.Content;
                section.ContentAr = dto.ContentAr;
                section.Order = dto.Order;

                if (section.Photos != null && section.Photos.Any())
                {
                    foreach (var photo in section.Photos)
                    {
                        var fullPath = Path.Combine(_uploadsFolder, photo.FileName);
                        if (File.Exists(fullPath))
                            File.Delete(fullPath);
                    }

                    _context.PageSectionPhoto.RemoveRange(section.Photos);
                }

                if (dto.Photos != null && dto.Photos.Count > 0)
                {
                    section.Photos = new List<PageSectionPhoto>();
                    foreach (var file in dto.Photos)
                    {
                        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                        var fullPath = Path.Combine(_uploadsFolder, fileName);

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        section.Photos.Add(new PageSectionPhoto
                        {
                            FileName = fileName,
                            FilePath = Path.Combine("uploads/sections", fileName)
                        });
                    }
                }

                await _context.SaveChangesAsync();
                result.Success = true;
                result.Message = "Done";
                result.Data = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Data = false;
                return result;
            }
            
        }


    }
}
