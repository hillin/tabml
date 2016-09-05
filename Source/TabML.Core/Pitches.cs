using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core
{
    public static class Pitches
    {
        public static Pitch C(int octaveGroup = Pitch.NeutralOctaveGroup) { return new Pitch(NoteNames.C, octaveGroup); }
        public static Pitch D(int octaveGroup = Pitch.NeutralOctaveGroup) { return new Pitch(NoteNames.D, octaveGroup); }
        public static Pitch E(int octaveGroup = Pitch.NeutralOctaveGroup) { return new Pitch(NoteNames.E, octaveGroup); }
        public static Pitch F(int octaveGroup = Pitch.NeutralOctaveGroup) { return new Pitch(NoteNames.F, octaveGroup); }
        public static Pitch G(int octaveGroup = Pitch.NeutralOctaveGroup) { return new Pitch(NoteNames.G, octaveGroup); }
        public static Pitch A(int octaveGroup = Pitch.NeutralOctaveGroup) { return new Pitch(NoteNames.A, octaveGroup); }
        public static Pitch B(int octaveGroup = Pitch.NeutralOctaveGroup) { return new Pitch(NoteNames.B, octaveGroup); }

        public static Pitch CSharp(int octaveGroup = Pitch.NeutralOctaveGroup) { return new Pitch(NoteNames.CSharp, octaveGroup); }
        public static Pitch DSharp(int octaveGroup = Pitch.NeutralOctaveGroup) { return new Pitch(NoteNames.DSharp, octaveGroup); }
        public static Pitch ESharp(int octaveGroup = Pitch.NeutralOctaveGroup) { return new Pitch(NoteNames.ESharp, octaveGroup); }
        public static Pitch FSharp(int octaveGroup = Pitch.NeutralOctaveGroup) { return new Pitch(NoteNames.FSharp, octaveGroup); }
        public static Pitch GSharp(int octaveGroup = Pitch.NeutralOctaveGroup) { return new Pitch(NoteNames.GSharp, octaveGroup); }
        public static Pitch ASharp(int octaveGroup = Pitch.NeutralOctaveGroup) { return new Pitch(NoteNames.ASharp, octaveGroup); }
        public static Pitch BSharp(int octaveGroup = Pitch.NeutralOctaveGroup) { return new Pitch(NoteNames.BSharp, octaveGroup); }

        public static Pitch CFlat(int octaveGroup = Pitch.NeutralOctaveGroup) { return new Pitch(NoteNames.CFlat, octaveGroup); }
        public static Pitch DFlat(int octaveGroup = Pitch.NeutralOctaveGroup) { return new Pitch(NoteNames.DFlat, octaveGroup); }
        public static Pitch EFlat(int octaveGroup = Pitch.NeutralOctaveGroup) { return new Pitch(NoteNames.EFlat, octaveGroup); }
        public static Pitch FFlat(int octaveGroup = Pitch.NeutralOctaveGroup) { return new Pitch(NoteNames.FFlat, octaveGroup); }
        public static Pitch GFlat(int octaveGroup = Pitch.NeutralOctaveGroup) { return new Pitch(NoteNames.GFlat, octaveGroup); }
        public static Pitch AFlat(int octaveGroup = Pitch.NeutralOctaveGroup) { return new Pitch(NoteNames.AFlat, octaveGroup); }
        public static Pitch BFlat(int octaveGroup = Pitch.NeutralOctaveGroup) { return new Pitch(NoteNames.BFlat, octaveGroup); }
    }
}
