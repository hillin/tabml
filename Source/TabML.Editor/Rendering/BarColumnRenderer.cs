using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;
using TabML.Core.MusicTheory;
using TabML.Core.MusicTheory.String.Plucked;
using TabML.Editor.Tablature;

namespace TabML.Editor.Rendering
{
    class BarColumnRenderer : ElementRenderer<BarColumn, BarRenderingContext>
    {

        public BarColumnRenderer(ElementRenderer owner, BarColumn element)
            : base(owner, element)
        {
        }

        public async Task PreRender()
        {
            var columnInfo = this.RenderingContext.ColumnRenderingInfos[this.Element.ColumnIndex];
            if (columnInfo.HasBrushlikeTechnique)
            {
                var beat = this.Element.VoiceBeats[0];
                int minString = int.MaxValue, maxString = int.MinValue;
                foreach (var note in this.Element.VoiceBeats.SelectMany(b => b.Notes))
                {
                    if (note.IsTied)
                        continue;

                    if (note.String < minString)
                        minString = note.String;
                    if (note.String > maxString)
                        maxString = note.String;
                }

                if (minString == int.MaxValue || maxString == int.MinValue)
                {

                    var chord = this.Element.Chord.Resolve(this.Element.OwnerBar.DocumentState);
                    if (chord != null)
                    {
                        var notes = chord.Fingering.Notes.ToArray();
                        var noteOfFirstString =
                            notes.FirstOrDefault(n => n.Fret != ChordFingeringNote.FingeringSkipString);

                        if (noteOfFirstString != null)
                        {
                            var noteOfLastString =
                                notes.LastOrDefault(n => n.Fret != ChordFingeringNote.FingeringSkipString);

                            // string index here is inverted
                            minString = this.RenderingContext.Style.StringCount - Array.IndexOf(notes, noteOfLastString) - 1;
                            maxString = this.RenderingContext.Style.StringCount - Array.IndexOf(notes, noteOfFirstString) - 1;
                        }
                    }
                }

                if (minString == int.MaxValue || maxString == int.MinValue)
                    return;

                double size;

                switch (beat.StrumTechnique)
                {
                    case StrumTechnique.BrushDown:
                        size = await this.RenderingContext.DrawInlineBrushDown(this.Element.ColumnIndex, minString, maxString);
                        break;
                    case StrumTechnique.BrushUp:
                        size = await this.RenderingContext.DrawInlineBrushUp(this.Element.ColumnIndex, minString, maxString);
                        break;
                    case StrumTechnique.ArpeggioDown:
                        size = await this.RenderingContext.DrawInlineArpeggioDown(this.Element.ColumnIndex, minString, maxString);
                        break;
                    case StrumTechnique.ArpeggioUp:
                        size = await this.RenderingContext.DrawInlineArpeggioUp(this.Element.ColumnIndex, minString, maxString);
                        break;
                    case StrumTechnique.Rasgueado:
                        size = await this.RenderingContext.DrawInlineRasgueado(this.Element.ColumnIndex, minString, maxString);
                        break;
                    case StrumTechnique.None:
                    case StrumTechnique.PickstrokeDown:
                    case StrumTechnique.PickstrokeUp:
                        throw new NotSupportedException();
                    default:
                        throw new NotSupportedException();
                }

                columnInfo.BrushlikeTechniqueSize = size;

                if (beat.IsTied)
                {
                    await NoteConnectionRenderer.DrawTie(this.Root, beat.PreviousBeat, beat, beat.GetOutmostStringIndex(beat.GetRenderTieVoicePart()),
                                                         beat.GetRenderTiePosition());
                }
            }
        }

        public async Task PostRender()
        {
            var columnInfo = this.RenderingContext.ColumnRenderingInfos[this.Element.ColumnIndex];

            if (this.Element.Lyrics != null)
            {
                await this.RenderingContext.DrawLyrics(columnInfo.Position, this.Element.Lyrics.Text);
            }

            if (this.Element.Chord != null && this.Element.IsFirstColumnOfSegment)
            {
                var chord = this.Element.Chord.Resolve(this.Element.OwnerBar.DocumentState);
                if (chord != null)  // todo: report error if chord is null
                {
                    await this.RenderingContext.DrawChord(columnInfo.Position, chord);
                }
            }
        }


        public async Task Render()
        {
            var notes =
                this.Element.VoiceBeats.SelectMany(
                        b => b.NotesDefiner.Notes.Select(n => new { n.String, NoteValue = b.NoteValue.Base }))
                    .OrderBy(n => n.String);

            var ellipseStrings = new List<int>();

            foreach (var note in notes)
            {
                if (note.NoteValue >= BaseNoteValue.Half)
                {
                    ellipseStrings.Add(note.String);
                    continue;
                }

                if (ellipseStrings.Count > 0)
                {
                    await this.RenderingContext.DrawEllipseAroundNotes(this.Element.ColumnIndex, ellipseStrings.ToArray());
                    ellipseStrings.Clear();
                }
            }

            if (ellipseStrings.Count > 0)
            {
                await this.RenderingContext.DrawEllipseAroundNotes(this.Element.ColumnIndex, ellipseStrings.ToArray());
            }
        }
    }
}
