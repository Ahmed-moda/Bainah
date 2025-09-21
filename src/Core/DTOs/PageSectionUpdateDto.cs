using Microsoft.AspNetCore.Http;

namespace CoreApi.DTOs
{
    public class PageSectionUpdateDto
    {
        public int Id { get; set; }
        public string Header { get; set; } = string.Empty;
        public string HeaderAr { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string ContentAr { get; set; } = string.Empty;
        public int Order { get; set; }
        public IFormFileCollection? Photos { get; set; }

    }
}
