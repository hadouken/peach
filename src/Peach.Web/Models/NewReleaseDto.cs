using System;
using System.ComponentModel.DataAnnotations;

namespace Peach.Web.Models
{
    public class NewReleaseDto
    {
        [Required]
        public string Version { get; set; }

        [Required]
        public Uri DownloadUri { get; set; }
    }
}