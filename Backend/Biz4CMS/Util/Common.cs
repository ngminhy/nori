using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biz4CMS.Util
{
    public static class Common
    {
        public static string GetOrderStatus(int st)
        {
            var ms = "";
            switch (st)
            {
                case 1:
                    ms = "Đặt hàng thành công";
                    break;
                case 2:
                    ms = "Đã tiếp nhận đơn hàng";
                    break;
                case 3:
                    ms = "Đang đóng gói";
                    break;
                case 4:
                    ms = "Đang vận chuyển";
                    break;
                case 5:
                    ms = "Giao hàng thành công";
                    break;
                default:
                    break;
            }
            return ms;
        }
    }
}