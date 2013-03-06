using System.Runtime.Serialization;

namespace UltimateTeam.Toolkit.Model
{
    [DataContract]
    internal class UserAccounts
    {
        [DataMember(Name = "userAccountInfo")]
        public UserAccountInfo UserAccountInfo { get; set; }
    }
}