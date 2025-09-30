using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bainah.Core.DTOs.Pages
{
    public class PageSectionDto
    {
        public int Id { get; set; }
        public string Header { get; set; } = default!;
        public string HeaderAr { get; set; } = default!;
        public string Content { get; set; } = default!;
        public string ContentAr { get; set; } = default!;
        public int Order { get; set; }
        public List<PageSectionPhotoDto> Photos { get; set; } = new();
    }
}
