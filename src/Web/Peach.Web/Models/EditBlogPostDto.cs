using System.ComponentModel.DataAnnotations;

namespace Peach.Web.Models
{
    public class EditBlogPostDto
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}