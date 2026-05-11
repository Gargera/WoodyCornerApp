using System;
using System.Collections.Generic;
using System.Text;
using WoodyCornerApp.DAL.Entities;

namespace WoodyCornerApp.BLL.Validation
{
    public static class ProductValidation
    {
        public static ValidResult Valid(this Product product)
        {
            var result = new ValidResult();
            result.valid = false;

            if (product == null) result.message = "product not found";
            else if (product.Name == null) result.message = "Product Name is Required";
            else if (product.Name.Length < 2 || product.Name.Length > 200) result.message = "Product Name must be between 2 and 200";
            else if (product.Description == null) result.message = "Product Description is Required"
            else if (product.Description.Length < 10 || product.Description.Length > 1000) result.message = "Product Description must be between 10 and 1000";
            else if (product.StockQuantity < 0 || product.StockQuantity > 1000000) result.message = "Product StockQuantity must be between 0 and 1000000";
            else if (product.ImagePath == null) result.message = "Product ImagePath is Required";
            else result.valid = true;

            return result;
        }
    }
}
