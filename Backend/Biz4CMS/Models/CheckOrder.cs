using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biz4CMS.Models
{
    public class CheckOrder
    {
        public string message { get; set; }
        public int status { get; set; }
        public string[] liststatus { get; set; }
        public string ordercode { get; set; }
    }
}