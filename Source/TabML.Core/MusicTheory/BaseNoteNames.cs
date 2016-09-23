namespace TabML.Core.MusicTheory
{
    public static class BaseNoteNames
    {

        public static bool TryParse(char noteNameChar, out BaseNoteName value)
        {
            switch (noteNameChar)
            {
                case 'c': case 'C': value = BaseNoteName.C; break;
                case 'd': case 'D': value = BaseNoteName.D; break;
                case 'e': case 'E': value = BaseNoteName.E; break;
                case 'f': case 'F': value = BaseNoteName.F; break;
                case 'g': case 'G': value = BaseNoteName.G; break;
                case 'a': case 'A': value = BaseNoteName.A; break;
                case 'b': case 'B': value = BaseNoteName.B; break;
                default:
                    value = BaseNoteName.C;
                    return false;
            }

            return true;
        }
    }
}
