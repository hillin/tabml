using TabML.Core.MusicTheory;

namespace TabML.Core.Document
{
    public struct BeatNote
    {
        public const int UnspecifiedFret = -1;

        public int String { get; }
        public int Fret { get; }
        public PreNoteConnection PreConnection { get; }
        public PostNoteConnection PostConnection { get; }

        public BeatNote(int stringNumber, int fret = UnspecifiedFret,
                        PreNoteConnection preConnection = PreNoteConnection.None,
                        PostNoteConnection postConnection = PostNoteConnection.None)
        {
            this.String = stringNumber;
            this.Fret = fret;
            this.PreConnection = preConnection;
            this.PostConnection = postConnection;
        }
    }
}
