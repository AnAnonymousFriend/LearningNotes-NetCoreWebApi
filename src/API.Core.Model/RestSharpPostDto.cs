using System;
using System.Collections.Generic;
using System.Text;

namespace API.Core.Model
{
    public class RestSharpPostDto
    {
        public bool result { get; set; }

        public string message { get; set; }

        public int status { get; set; }

        public object data { get; set; }
    }
}
