using System.ComponentModel.DataAnnotations;

namespace NewsProject.Models
{
    public class News
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Max length is 30 characters")]
        public string ShortDescription { get; set; }

        [Required]
        public string Content { get; set; }

        public string Image { get; set; }
    }
}
