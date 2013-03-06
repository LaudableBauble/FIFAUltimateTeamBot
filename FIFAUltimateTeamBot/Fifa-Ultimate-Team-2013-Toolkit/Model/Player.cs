namespace UltimateTeam.Toolkit.Model
{
    internal class Player
    {
        public int Id { get; set; }

        public long NucleusId { get; set; }

        public string Email { get; set; }

        public PreferredPersona PreferredPersona { get; set; }
    }
}