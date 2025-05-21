using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Helper
{
    public class CustomException : Exception
    {
        public int StatusCode { get; }
        public object? Data{ get; }
        public string Message { get; set; }

        public CustomException(int statusCode, string msg, object data)
            : base(msg)
        {
            StatusCode = statusCode;
            Message= msg;
            Data = data;
        }
    }
}
