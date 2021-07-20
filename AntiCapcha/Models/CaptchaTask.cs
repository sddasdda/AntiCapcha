using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiCaptcha
{
    class CaptchaTask
    {
        public string type { get; set; }
        public string body { get; set; }
        public bool phrase { get; set; }
        public bool @case { get; set; }
        public bool numeric { get; set; }
    }
}
