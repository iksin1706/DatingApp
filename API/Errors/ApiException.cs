using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiException
    {
        public ApiException(int statusCode, string messege, string details)
        {
            StatusCode = statusCode;
            Messege = messege;
            Details = details;
        }

        public int StatusCode { get; set; }
        public string Messege { get; set; }
        public string Details { get; set; }
    }
}