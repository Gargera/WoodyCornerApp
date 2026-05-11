using System;
using System.Collections.Generic;
using System.Text;

namespace WoodyCornerApp.BLL.Common
{
    public class ServiceResult<T> where T : class
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "Done";
        public T Data { get; set; } = null!;
    }
}
