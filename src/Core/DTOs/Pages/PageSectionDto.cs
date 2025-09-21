using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Pages
{
    public class PageSectionDto
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string HeaderAr { get; set; }
        public string Content { get; set; }
        public string ContentAr { get; set; }
        public int Order { get; set; }
        public List<PageSectionPhotoDto> Photos { get; set; } = new();
    }
}
