using System.ComponentModel.DataAnnotations;

namespace Peach.Web.Models
{
    public class NewBlogPostDto
    {
        [Required, MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}