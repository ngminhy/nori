using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biz4CMS.ViewModels
{
    public class BriefArticleDto
    {
        public int ArticleId { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public String MainImage { get; set; }
        public String PageURL { get; set; }
    }
}