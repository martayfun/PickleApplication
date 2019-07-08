using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace PickleApplication.Web.Core
{
    public class ApiResponse<T>
    {
        public ApiResponse(HttpStatusCode statusCode, T data, string message = null, bool success = false, string responseTime = "0")
        {
            StatusCode = (int)statusCode;
            Data = data;
            Message = message;
            Success = success;
            ResponseTime = responseTime;
        }

        /// <summary>
        /// Version of Api used
        /// </summary>
        public string Version { get { return "1"; } }

        /// <summary>
        /// Status Code of the Request
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Error Code of the Request
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Generic Object served as result
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Response Api time in miliseconds
        /// </summary>
        public string ResponseTime { get; set; }

        /// <summary>
        /// Succes or not
        /// </summary>
        public bool Success { get; set; }
    }
}