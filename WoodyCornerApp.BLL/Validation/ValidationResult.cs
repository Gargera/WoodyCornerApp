using System;
using System.Collections.Generic;
using System.Text;

namespace WoodyCornerApp.BLL.Validation
{
    public class ValidationResult
    {
        public bool valid { get; set; } = true;
        public string message { get; set; } = "Valid";
    }
}
