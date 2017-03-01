namespace Core.Smufl {

    import Glyphes = Core.Smufl.Metadata.Glyphes;
    import GlyphKey = Core.Smufl.Metadata.GlyphKey;
    import OffBarDirection = Core.MusicTheory.OffBarDirection;
    import BeatModifier = Core.MusicTheory.BeatModifier;
    import BaseNoteValue = Core.MusicTheory.BaseNoteValue;

    export class Utilities {

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
            let codePt = typeof name === "string" ? Glyphes[name].codepoint : name;
            return Utilities.fixedFromCharCode(codePt);
        }

        static GetNumber(value: number, base: number): string {
            return value.toString().split('').map((c: string) => Utilities.fixedFromCharCode(parseInt(c) + base)).join();
        }

        static GetTimeSignatureNumber(value: number): string {
            return Utilities.GetNumber(value, Glyphes['timeSig0'].codepoint);
        }

        static GetTupletNumber(value: number): string {
            return Utilities.GetNumber(value, Glyphes['tuplet0'].codepoint);
        }

        static GetNoteValue(noteValue: BaseNoteValue): string {
            switch (noteValue) {
                case BaseNoteValue.Large:
                    return Utilities.GetCharacter('noteDoubleWhole');    //todo: smufl does not provide an individual large note
                case BaseNoteValue.Long:
                    return Utilities.GetCharacter('noteDoubleWhole');    //todo: smufl does not provide an individual large note
                case BaseNoteValue.Double:
                    return Utilities.GetCharacter('noteDoubleWhole');
                case BaseNoteValue.Whole:
                    return Utilities.GetCharacter('noteWhole');
                case BaseNoteValue.Half:
                    return Utilities.GetCharacter('noteHalfUp');
                case BaseNoteValue.Quater:
                    return Utilities.GetCharacter('noteQuarterUp');
                case BaseNoteValue.Eighth:
                    return Utilities.GetCharacter('note8thUp');
                case BaseNoteValue.Sixteenth:
                    return Utilities.GetCharacter('note16thUp');
                case BaseNoteValue.ThirtySecond:
                    return Utilities.GetCharacter('note32ndUp');
                case BaseNoteValue.SixtyFourth:
                    return Utilities.GetCharacter('note64thUp');
                case BaseNoteValue.HundredTwentyEighth:
                    return Utilities.GetCharacter('note128thUp');
                case BaseNoteValue.TwoHundredFiftySixth:
                    return Utilities.GetCharacter('note256thUp');
            }
        }

        static GetBeatModifier(beatModifier: BeatModifier, direction: OffBarDirection): string {
            switch (beatModifier) {
                case BeatModifier.Accent:
                    return Utilities.GetCharacter(direction == OffBarDirection.Top ? "articAccentAbove" : "articAccentBelow");
                case BeatModifier.Marcato:
                    return Utilities.GetCharacter(direction == OffBarDirection.Top ? "articMarcatoAbove" : "articMarcatoBelow");
                case BeatModifier.Staccato:
                    return Utilities.GetCharacter(direction == OffBarDirection.Top ? "articStaccatoAbove" : "articStaccatoBelow");
                case BeatModifier.Staccatissimo:
                    return Utilities.GetCharacter(direction == OffBarDirection.Top ? "articStaccatissimoAbove" : "articStaccatissimoBelow");
                case BeatModifier.Tenuto:
                    return Utilities.GetCharacter(direction == OffBarDirection.Top ? "articTenutoAbove" : "articTenutoBelow");
                case BeatModifier.Fermata:
                    return Utilities.GetCharacter(direction == OffBarDirection.Top ? "fermataAbove" : "fermataBelow");
                case BeatModifier.PickstrokeDown:
                    return Utilities.GetCharacter("stringsDownBow");
                case BeatModifier.PickstrokeUp:
                    return Utilities.GetCharacter("stringsUpBow");
                case BeatModifier.Trill:
                    return Utilities.GetCharacter("ornamentTrill");
                case BeatModifier.Mordent:
                    return Utilities.GetCharacter("ornamentMordent");
                case BeatModifier.LowerMordent:
                    return Utilities.GetCharacter("ornamentMordentInverted");
                case BeatModifier.Turn:
                    return Utilities.GetCharacter("ornamentTurn");
                case BeatModifier.InvertedTurn:
                    return Utilities.GetCharacter("ornamentTurnInverted");
            }
        }

        static GetRest(noteValue: BaseNoteValue): string {
            return Utilities.GetCharacter(Glyphes['restWhole'].codepoint - noteValue);
        }
    }
}