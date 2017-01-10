using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biz4CMS.ViewModels
{
    public class BoxLinkDto
    {
        public string BoxName { get; set; }
        public string BoxLink { get; set; }
        public string BoxType { get; set; }
        public List<LinkDto> Links { get; set; }
    }
}