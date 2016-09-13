using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;

namespace TabML.Core.Parsing
{
    static class Common
    {
        public static bool TryParseNoteName(Scanner scanner, out NoteName noteName)
        {
            var noteNameChar = scanner.Read();
            BaseNoteName baseNoteName;
            if (!BaseNoteNames.TryParse(noteNameChar, out baseNoteName))
            {
                noteName = default(NoteName);
                return false;
            }

            var accidentalText = scanner.Read("[#b♯♭\u1d12a\u1d12b]*");
            Accidental accidental;
            Accidentals.TryParse(accidentalText, out accidental);

            noteName = new NoteName(baseNoteName, accidental);
            return true;
        }
    }
}
