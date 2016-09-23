namespace TabML.Core.MusicTheory
{
    public struct CapoInfo
    {
        public static readonly CapoInfo NoCapo = new CapoInfo(0);

        public const int[] AffectAllStrings = null;

        public int Position { get; }
        public int[] AffectedStrings { get; }

        public CapoInfo(int position, int[] affectedStrings = null)
        {
            this.Position = position;
            this.AffectedStrings = affectedStrings;
        }
    }
}
