using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Parameter
{
    public class Formation : SearchParameterBase<string>
    {
        public const string ThreeFourOneTwo = "f3412";
        public const string ThreeFourTwoOne = "f3421";
        public const string ThreeFourThree = "f343";
        public const string ThreeFiveTwo = "f352";
        public const string FourOneTwoOneTwo = "f41212";
        public const string FourTwoThreeOne = "f4231";
        public const string FourTwoTwoTwo = "f4222";
        public const string FourThreeOneTwo = "f4312";
        public const string FourThreeTwoOne = "f4321";
        public const string FourThreeThree = "f433";
        public const string FourFourOneOne = "f4411";
        public const string FourFourTwo = "f442";
        public const string FourFiveOne = "f451";
        public const string FiveTwoOneTwo = "f5212";
        public const string FiveTwoTwoOne = "f5221";
        public const string FiveThreeTwo = "f532";

        private Formation(string description, string value)
        {
            Description = description;
            Value = value;
        }

        public static IEnumerable<Formation> GetAll()
        {
            yield return new Formation("3-4-1-2", ThreeFourOneTwo);
            yield return new Formation("3-4-2-1", ThreeFourTwoOne);
            yield return new Formation("3-4-3", ThreeFourThree);
            yield return new Formation("3-5-2", ThreeFiveTwo);
            yield return new Formation("4-1-2-1-2", FourOneTwoOneTwo);
            yield return new Formation("4-2-3-1", FourTwoThreeOne);
            yield return new Formation("4-2-2-2", FourTwoTwoTwo);
            yield return new Formation("4-3-1-2", FourThreeOneTwo);
            yield return new Formation("4-3-2-1", FourThreeTwoOne);
            yield return new Formation("4-3-3", FourThreeThree);
            yield return new Formation("4-4-1-1", FourFourOneOne);
            yield return new Formation("4-4-2", FourFourTwo);
            yield return new Formation("4-5-1", FourFiveOne);
            yield return new Formation("5-2-1-2", FiveTwoOneTwo);
            yield return new Formation("5-2-2-1", FiveTwoTwoOne);
            yield return new Formation("5-3-2", FiveThreeTwo);
        }
    }
}