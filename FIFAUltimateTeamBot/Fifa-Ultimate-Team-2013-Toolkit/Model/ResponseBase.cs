using System.Runtime.Serialization;

namespace UltimateTeam.Toolkit.Model
{
    /// <summary>
    /// The base response class. Contains the data and possibly an error message.
    /// </summary>    
    [DataContract]
    public class ResponseBase
    {
        public ErrorResponse Error { get; set; }

        public bool HasFailed()
        {
            return Error != null;
        }
    }
}