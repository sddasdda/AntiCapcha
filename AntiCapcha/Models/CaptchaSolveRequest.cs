using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiCaptcha
{ 
    class CaptchaSolveRequest
    {
        public string clientKey { get; set; }
        public CaptchaTask task { get; set; }
    }
}
