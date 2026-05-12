using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using WoodyCornerApp.DAL.Entities;

namespace WoodyCornerApp.BLL.Validation
{
    public static class OrderValidation
    {
        public static ValidationResult Valid(this Order order)
        {
            var result = new ValidationResult();
            result.valid = false;

            if (order == null) result.message = "order not found";
            else if(order.OrderStatus < OrderStatus.Pending || order.OrderStatus > OrderStatus.Cancelled) result.message = "Invalid Order Status";
            else if (order.ShippingAddress.Length < 10 || order.ShippingAddress.Length > 300) result.message = "Shipping Address must be between 10 and 300 characters";
            else if (order.ShippingCity == null) result.message = "Shipping City is Required";
            else if (order.ShippingCity.Length < 3 || order.ShippingCity.Length > 100) result.message = "Shipping City must be between 3 and 100 characters";
            else result.valid = true;

            return result;
        }
    }
}
