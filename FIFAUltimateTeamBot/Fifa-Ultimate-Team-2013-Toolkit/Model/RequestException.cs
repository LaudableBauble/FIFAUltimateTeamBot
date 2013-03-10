using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTeam.Toolkit.Model
{
    [Serializable]
    public class RequestException : Exception, ISerializable
    {
        public ErrorResponse Error { get; set; }

        public RequestException() : base("A request has failed!") { }
        public RequestException(ErrorResponse error) : base()
        {
            Error = error;
        }
        public RequestException(string message) : base(message) { }
        public RequestException(string message, Exception inner) : base(message, inner) { }
        protected RequestException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}