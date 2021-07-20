using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiCaptcha
{
    class CaptchaSolveResponse
    {
        public int errorId { get; set; }
        public string errorDescription { get; set; }
        public int taskId { get; set; }
    }
}
