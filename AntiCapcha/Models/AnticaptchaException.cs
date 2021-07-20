using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiCaptcha
{
    class AnticaptchaException : Exception
    {
        public AnticaptchaException() { }
        public AnticaptchaException(string message) : base(message) { }
        public AnticaptchaException(string message, Exception inner) : base(message, inner) { }
        protected AnticaptchaException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
