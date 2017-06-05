using InterviewTest.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewTest.Framework
{
    public class HttpRequest : IHttpRequest
    {
        public string Url { get; set; }
    }
}
