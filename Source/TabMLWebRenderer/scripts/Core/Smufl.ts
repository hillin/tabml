namespace Core {

    const SmuflGlyphes: any = {
        //Time signatures (U+E080 – U+E09F)
        'timeSig0': 0xE080,
        'timeSig1': 0xE081,
        'timeSig2': 0xE082,
        'timeSig3': 0xE083,
        'timeSig4': 0xE084,
        'timeSig5': 0xE085,
        'timeSig6': 0xE086,
        'timeSig7': 0xE087,
        'timeSig8': 0xE088,
        'timeSig9': 0xE089,
        'timeSigCommon': 0xE08A,

        //Individual notes (U+E1D0 – U+E1EF)
        'noteDoubleWhole': 0xE1D0,
        'noteDoubleWholeSquare': 0xE1D1,
        'noteWhole': 0xE1D2,
        'noteHalfUp': 0xE1D3,
        'noteHalfDown': 0xE1D4,
        'noteQuarterUp': 0xE1D5,
        'noteQuarterDown': 0xE1D6,
        'note8thUp': 0xE1D7,
        'note8thDown': 0xE1D8,
        'note16thUp': 0xE1D9,
        'note16thDown': 0xE1DA,
        'note32ndUp': 0xE1DB,
        'note32ndDown': 0xE1DC,
        'note64thUp': 0xE1DD,
        'note64thDown': 0xE1DE,
        'note128thUp': 0xE1DF,
        'note128thDown': 0xE1E0,
        'note256thUp': 0xE1E1,
        'note256thDown': 0xE1E2,
        'note512thUp': 0xE1E3,
        'note512thDown': 0xE1E4,
        'note1024thUp': 0xE1E5,
        'note1024thDown': 0xE1E6,
        'augmentationDot': 0xE1E7,


        //Chord diagrams (U+E850 ‒ U+E85F)
        'fretboardFilledCircle': 0xE858,
        'fretboardX': 0xE859,
        'fretboardO': 0xE85A,

        //Tuplets (U+E880 – U+E88F)
        'tuplet0': 0XE880,
        'tuplet1': 0XE881,
        'tuplet2': 0XE882,
        'tuplet3': 0XE883,
        'tuplet4': 0XE884,
        'tuplet5': 0XE885,
        'tuplet6': 0XE886,
        'tuplet7': 0XE887,
        'tuplet8': 0XE888,
        'tuplet9': 0XE889,
        'tupletColon': 0xE88A

    };

    type GlyphKey = keyof typeof SmuflGlyphes;

    export class Smufl {


        private static fixedFromCharCode(codePt: number): string {
            if (codePt > 0xFFFF) {
                codePt -= 0x10000;
                return String.fromCharCode(0xD800 + (codePt >> 10), 0xDC00 + (codePt & 0x3FF));
            }
            else {
                return String.fromCharCode(codePt);
            }
        }

        static GetCharacter(name: GlyphKey | number): string {
            let codePt = typeof name === "string" ? SmuflGlyphes[name] : name;
            return Smufl.fixedFromCharCode(codePt);
        }

        static GetNumber(value: number, base: number): string {
            return value.toString().split('').map((c: string) => Smufl.fixedFromCharCode(parseInt(c) + base)).join();
        }

        static GetTimeSignatureNumber(value: number): string {
            return Smufl.GetNumber(value, SmuflGlyphes['timeSig0']);
        }

        static GetTupletNumber(value: number): string {
            return Smufl.GetNumber(value, SmuflGlyphes['tuplet0']);
        }

        static GetNoteValue(noteValue: BaseNoteValue): string {
            switch (noteValue) {
                case BaseNoteValue.Large:
                    return Smufl.GetCharacter('noteDoubleWhole');    //todo: smufl does not provide an individual large note
                case BaseNoteValue.Long:
                    return Smufl.GetCharacter('noteDoubleWhole');    //todo: smufl does not provide an individual large note
                case BaseNoteValue.Double:
                    return Smufl.GetCharacter('noteDoubleWhole');
                case BaseNoteValue.Whole:
                    return Smufl.GetCharacter('noteWhole');
                case BaseNoteValue.Half:
                    return Smufl.GetCharacter('noteHalfUp');
                case BaseNoteValue.Quater:
                    return Smufl.GetCharacter('noteQuarterUp');
                case BaseNoteValue.Eighth:
                    return Smufl.GetCharacter('note8thUp');
                case BaseNoteValue.Sixteenth:
                    return Smufl.GetCharacter('note16thUp');
                case BaseNoteValue.ThirtySecond:
                    return Smufl.GetCharacter('note32ndUp');
                case BaseNoteValue.SixtyFourth:
                    return Smufl.GetCharacter('note64thUp');
                case BaseNoteValue.HundredTwentyEighth:
                    return Smufl.GetCharacter('note128thUp');
                case BaseNoteValue.TwoHundredFiftySixth:
                    return Smufl.GetCharacter('note256thUp');
            }
        }
    }
}