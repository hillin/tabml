using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core
{
    public static class AbsoluteNoteNames
    {
        private static readonly Dictionary<int, Dictionary<NoteName, AbsoluteNoteName>> CachedNames
            = new Dictionary<int, Dictionary<NoteName, AbsoluteNoteName>>();

        private static AbsoluteNoteName GetAbsoluteNoteName(NoteName noteName, int octaveGroup)
        {
            Dictionary<NoteName, AbsoluteNoteName> dict;
            AbsoluteNoteName absoluteNoteName;
            if (CachedNames.TryGetValue(octaveGroup, out dict))
            {
                if(dict.TryGetValue(noteName, out absoluteNoteName))
                    return absoluteNoteName;
                
                absoluteNoteName = new AbsoluteNoteName(noteName, octaveGroup);
                dict[noteName] = absoluteNoteName;
                return absoluteNoteName;
            }
            
            dict = new Dictionary<NoteName, AbsoluteNoteName>();
            absoluteNoteName = new AbsoluteNoteName(noteName, octaveGroup);
            dict[noteName] = absoluteNoteName;
            return absoluteNoteName;
        }

        public static AbsoluteNoteName C(int octaveGroup = AbsoluteNoteName.NeutralOctaveGroup) { return GetAbsoluteNoteName(NoteName.C, octaveGroup); }
        public static AbsoluteNoteName D(int octaveGroup = AbsoluteNoteName.NeutralOctaveGroup) { return GetAbsoluteNoteName(NoteName.D, octaveGroup); }
        public static AbsoluteNoteName E(int octaveGroup = AbsoluteNoteName.NeutralOctaveGroup) { return GetAbsoluteNoteName(NoteName.E, octaveGroup); }
        public static AbsoluteNoteName F(int octaveGroup = AbsoluteNoteName.NeutralOctaveGroup) { return GetAbsoluteNoteName(NoteName.F, octaveGroup); }
        public static AbsoluteNoteName G(int octaveGroup = AbsoluteNoteName.NeutralOctaveGroup) { return GetAbsoluteNoteName(NoteName.G, octaveGroup); }
        public static AbsoluteNoteName A(int octaveGroup = AbsoluteNoteName.NeutralOctaveGroup) { return GetAbsoluteNoteName(NoteName.A, octaveGroup); }
        public static AbsoluteNoteName B(int octaveGroup = AbsoluteNoteName.NeutralOctaveGroup) { return GetAbsoluteNoteName(NoteName.B, octaveGroup); }

        public static AbsoluteNoteName CSharp(int octaveGroup = AbsoluteNoteName.NeutralOctaveGroup) { return GetAbsoluteNoteName(NoteName.CSharp, octaveGroup); }
        public static AbsoluteNoteName DSharp(int octaveGroup = AbsoluteNoteName.NeutralOctaveGroup) { return GetAbsoluteNoteName(NoteName.DSharp, octaveGroup); }
        public static AbsoluteNoteName ESharp(int octaveGroup = AbsoluteNoteName.NeutralOctaveGroup) { return GetAbsoluteNoteName(NoteName.ESharp, octaveGroup); }
        public static AbsoluteNoteName FSharp(int octaveGroup = AbsoluteNoteName.NeutralOctaveGroup) { return GetAbsoluteNoteName(NoteName.FSharp, octaveGroup); }
        public static AbsoluteNoteName GSharp(int octaveGroup = AbsoluteNoteName.NeutralOctaveGroup) { return GetAbsoluteNoteName(NoteName.GSharp, octaveGroup); }
        public static AbsoluteNoteName ASharp(int octaveGroup = AbsoluteNoteName.NeutralOctaveGroup) { return GetAbsoluteNoteName(NoteName.ASharp, octaveGroup); }
        public static AbsoluteNoteName BSharp(int octaveGroup = AbsoluteNoteName.NeutralOctaveGroup) { return GetAbsoluteNoteName(NoteName.BSharp, octaveGroup); }

        public static AbsoluteNoteName CFlat(int octaveGroup = AbsoluteNoteName.NeutralOctaveGroup) { return GetAbsoluteNoteName(NoteName.CFlat, octaveGroup); }
        public static AbsoluteNoteName DFlat(int octaveGroup = AbsoluteNoteName.NeutralOctaveGroup) { return GetAbsoluteNoteName(NoteName.DFlat, octaveGroup); }
        public static AbsoluteNoteName EFlat(int octaveGroup = AbsoluteNoteName.NeutralOctaveGroup) { return GetAbsoluteNoteName(NoteName.EFlat, octaveGroup); }
        public static AbsoluteNoteName FFlat(int octaveGroup = AbsoluteNoteName.NeutralOctaveGroup) { return GetAbsoluteNoteName(NoteName.FFlat, octaveGroup); }
        public static AbsoluteNoteName GFlat(int octaveGroup = AbsoluteNoteName.NeutralOctaveGroup) { return GetAbsoluteNoteName(NoteName.GFlat, octaveGroup); }
        public static AbsoluteNoteName AFlat(int octaveGroup = AbsoluteNoteName.NeutralOctaveGroup) { return GetAbsoluteNoteName(NoteName.AFlat, octaveGroup); }
        public static AbsoluteNoteName BFlat(int octaveGroup = AbsoluteNoteName.NeutralOctaveGroup) { return GetAbsoluteNoteName(NoteName.BFlat, octaveGroup); }
    }
}
