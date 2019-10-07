using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Helpers
{
    public class HttpResponseBaseModel
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public Exception Exception { get; set; }
    }
}