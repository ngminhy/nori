using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biz4CMS.Models
{
  
        public partial class Order
        {
            public int OrderId { get; set; }
            public string Username { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string FullName { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string PostalCode { get; set; }
            public string Country { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public decimal Total { get; set; }
            public string Note { get; set; }
            public decimal PaymentType { get; set; }
            public string BranchName { get; set; }
            public string BookingTime { get; set; }
            public string OrderCode { get; set; }
            public int OrderStatusId { get; set; }
            public System.DateTime OrderDate { get; set; }
            public List<OrderDetail> OrderDetails { get; set; }
        }
}