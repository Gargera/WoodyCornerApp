using System;
using System.Collections.Generic;
using System.Text;

namespace WoodyCornerApp.DAL.Entities
{
    public enum OrderStatus
    {
        Pending = 0,
        Processing = 1,
        Shipped = 2,
        Delivered = 3,
        Cancelled = 4
    }
}
