using Bainah.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class PageSectionPhoto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; } = string.Empty;

        [Required]
        public string FilePath { get; set; } = string.Empty; // relative path like "uploads/sections/file.jpg"

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        // Foreign Key
        [ForeignKey(nameof(PageSection))]
        public int PageSectionId { get; set; }

        public PageSection PageSection { get; set; } = default!;
    }
}
