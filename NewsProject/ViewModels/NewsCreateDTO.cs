using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace NewsProject.ViewModels
{
    public class NewsCreateDTO
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Max length is 30 characters")]
        public string ShortDescription { get; set; }

        [Required]
        public string Content { get; set; }

        public IFormFile ImageFile { get; set; }
    }
}
