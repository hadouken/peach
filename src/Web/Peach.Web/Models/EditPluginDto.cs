using System;
using System.ComponentModel;

namespace Peach.Web.Models
{
    public class EditPluginDto
    {
        [ReadOnly(true)]
        public string Name { get; set; }

        public Uri Homepage { get; set; }

        public string Description { get; set; }
    }
}