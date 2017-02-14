using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biz4CMS.ViewModels
{
    public class UserInfo
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string BookingTime { get; set; }
        public int Branch { get; set; }
        public string BranchName { get; set; }
        public string Note { get; set; }
    }
}