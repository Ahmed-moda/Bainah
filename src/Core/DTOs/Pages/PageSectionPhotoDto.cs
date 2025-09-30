using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bainah.Core.DTOs.Pages
{
    public class PageSectionPhotoDto
    {
        public int Id { get; set; }
        public string FileName { get; set; } = default!;
        public string FilePath { get; set; } = default!;
    }
}
