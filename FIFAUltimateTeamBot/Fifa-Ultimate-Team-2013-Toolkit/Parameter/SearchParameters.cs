using UltimateTeam.Toolkit.Model;

namespace UltimateTeam.Toolkit.Parameter
{
    public abstract class SearchParameters
    {
        protected SearchParameters(ResourceType type)
        {
            Type = type;
            Page = 1;
        }

        protected ResourceType Type { get; private set; }

        public uint Page { get; set; }

        internal abstract string BuildUriString(ref string uriString);
    }
}