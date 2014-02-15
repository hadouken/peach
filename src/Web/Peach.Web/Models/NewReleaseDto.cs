using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Peach.Web.Models
{
    public class NewReleaseDto
    {
        [Required]
        public string Version { get; set; }

        [Required]
        public HttpPostedFileBase[] AttachedFiles { get; set; }

        [Required]
        public string ReleaseNotes { get; set; }
    }
}