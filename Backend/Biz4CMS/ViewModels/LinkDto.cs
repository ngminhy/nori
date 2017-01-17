using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biz4CMS.ViewModels
{
    public class LinkDto
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public int MenuId { get; set; }
        public List<LinkDto> SubMenus;
    }
}