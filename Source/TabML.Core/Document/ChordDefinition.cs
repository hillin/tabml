namespace TabML.Core.Document
{
    public class ChordDefinition
    {
        public const int FingeringSkipString = -1;

        public string Name { get; }
        public string DisplayName { get; }
        public int[] Fingering { get; }

        public ChordDefinition(string name, string displayName = null, int[] fingering = null)
        {
            this.Name = name;
            this.DisplayName = displayName;
            this.Fingering = fingering;
        }
    }
}
