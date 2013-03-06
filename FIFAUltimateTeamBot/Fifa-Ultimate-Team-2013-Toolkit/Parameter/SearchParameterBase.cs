namespace UltimateTeam.Toolkit.Parameter
{
    public abstract class SearchParameterBase<TValue>
    {
        public string Description { get; set; }

        public TValue Value { get; set; }
    }
}