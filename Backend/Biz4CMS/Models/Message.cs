using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Biz4CMS.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        [Required]
        [StringLength(200)]
        public string FullName { get; set; }
        [Required]
        [StringLength(200)]
        public string Email { get; set; }
        [Required]
        [StringLength(2000)]
        public string Content { get; set; }
        public DateTime  CreatedDate { get; set; }       
    }
}
