using Bainah.Core.DTOs.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Pages
{
    public class WebPageDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public List<PageSectionDto> Sections { get; set; }
    }
}
