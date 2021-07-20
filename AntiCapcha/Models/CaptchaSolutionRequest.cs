using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiCaptcha
{
    class CaptchaSolutionRequest
    {
        public string clientKey { get; set; }
        public int taskId { get; set; }
    }
}
