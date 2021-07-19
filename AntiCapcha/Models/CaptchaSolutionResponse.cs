using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiCapcha
{
    class CaptchaSolutionResponse
    {
        public int errorId { get; set; }
        public string errorDescription { get; set; }
        public string status { get; set; }
        public CaptchaSolution solution { get; set; }
    }
}
