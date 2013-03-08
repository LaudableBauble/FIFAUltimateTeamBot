using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FIFAUltimateTeamBot
{
    class RequestException : Exception, ISerializable
    {
        public RequestException() : this("A request has failed!") { }
        public RequestException(string message)
        {
            // Add implementation.
        }
        public RequestException(string message, Exception inner)
        {
            // Add implementation.
        }

        // This constructor is needed for serialization.
        protected RequestException(SerializationInfo info, StreamingContext context)
        {
            // Add implementation.
        }
    }
}
