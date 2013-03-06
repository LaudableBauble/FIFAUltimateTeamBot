using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Parameter
{
    public class Nation : SearchParameterBase<uint>
    {
        public const uint Argentina = 52;

        public const uint Brazil = 54;

        public const uint England = 14;

        public const uint France = 18;

        public const uint Germany = 21;

        public const uint Italy = 27;

        public const uint Netherlands = 34;

        public const uint Norway = 36;

        public const uint Portugal = 38;

        public const uint Spain = 45;

        private Nation(string description, uint value)
        {
            Description = description;
            Value = value;
        }

        public static IEnumerable<Nation> GetAll()
        {
            yield return new Nation("Argentina", Argentina);
            yield return new Nation("Brazil", Brazil);
            yield return new Nation("England", England);
            yield return new Nation("France", France);
            yield return new Nation("Germany", Germany);
            yield return new Nation("Italy", Italy);
            yield return new Nation("Netherlands", Netherlands);
            yield return new Nation("Norway", Norway);
            yield return new Nation("Portugal", Portugal);
            yield return new Nation("Spain", Spain);
        }
    }
}