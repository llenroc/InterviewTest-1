using System;
using InterviewTest.Abstractions;

namespace InterviewTest.Framework
{
    public class HttpResponse : IHttpResponse
    {
        public string Data { get; set; }

        public bool IsSuccess { get; set; }
    }
}
