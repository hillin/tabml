using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TabML.Core.BaseNoteName;

namespace TabML.Core
{
    public static class NoteNames
    {
        public static readonly NoteName C = new NoteName(BaseNoteName.C, Accidental.Natural);
        public static readonly NoteName D = new NoteName(BaseNoteName.D, Accidental.Natural);
        public static readonly NoteName E = new NoteName(BaseNoteName.E, Accidental.Natural);
        public static readonly NoteName F = new NoteName(BaseNoteName.F, Accidental.Natural);
        public static readonly NoteName G = new NoteName(BaseNoteName.G, Accidental.Natural);
        public static readonly NoteName A = new NoteName(BaseNoteName.A, Accidental.Natural);
        public static readonly NoteName B = new NoteName(BaseNoteName.B, Accidental.Natural);

        public static readonly NoteName CSharp = new NoteName(BaseNoteName.C, Accidental.Sharp);
        public static readonly NoteName DSharp = new NoteName(BaseNoteName.D, Accidental.Sharp);
        public static readonly NoteName ESharp = new NoteName(BaseNoteName.E, Accidental.Sharp);
        public static readonly NoteName FSharp = new NoteName(BaseNoteName.F, Accidental.Sharp);
        public static readonly NoteName GSharp = new NoteName(BaseNoteName.G, Accidental.Sharp);
        public static readonly NoteName ASharp = new NoteName(BaseNoteName.A, Accidental.Sharp);
        public static readonly NoteName BSharp = new NoteName(BaseNoteName.B, Accidental.Sharp);

        public static readonly NoteName CFlat = new NoteName(BaseNoteName.C, Accidental.Flat);
        public static readonly NoteName DFlat = new NoteName(BaseNoteName.D, Accidental.Flat);
        public static readonly NoteName EFlat = new NoteName(BaseNoteName.E, Accidental.Flat);
        public static readonly NoteName FFlat = new NoteName(BaseNoteName.F, Accidental.Flat);
        public static readonly NoteName GFlat = new NoteName(BaseNoteName.G, Accidental.Flat);
        public static readonly NoteName AFlat = new NoteName(BaseNoteName.A, Accidental.Flat);
        public static readonly NoteName BFlat = new NoteName(BaseNoteName.B, Accidental.Flat);
    }
}
