import BarLine = Core.MusicTheory.BarLine;
import BaseNoteValue = Core.MusicTheory.BaseNoteValue;
import OffBarDirection = Core.MusicTheory.OffBarDirection;
import NoteValueAugment = Core.MusicTheory.NoteValueAugment;
import GlissDirection = Core.MusicTheory.GlissDirection;
import NoteRenderingFlags = TR.NoteRenderingFlags;
import Smufl = Core.Smufl;

type point = { x: number, y: number };

interface CallbackObject {
    callback(result: any): void;
}

declare var __callbackObject: CallbackObject;

namespace TR {

    export interface IBoundingBox {
        left: number; top: number; width: number; height: number;
    }

    export class PrimitiveRenderer {


        private canvas: fabric.IStaticCanvas;
        private style: ITablatureStyle;

        constructor(canvas: fabric.IStaticCanvas, style: ITablatureStyle) {
            this.canvas = canvas;
            this.style = style;
            this.clear();
        }

        clear() {
            this.canvas.clear();
            this.canvas.backgroundColor = "white";
        }

        private getScale(): number {
            return this.style.bar.lineHeight / ResourceManager.referenceBarSpacing;
        }

        private getSmuflTextStyle(size: number) {
            return {
                fontFamily: this.style.smuflText.fontFamily,
                fontSize: size
            };
        }

        private static inflateBounds(bounds: IBoundingBox, extension: IBoundingBox): IBoundingBox {
            if (bounds === undefined)
                return extension;

            let left = Math.min(bounds.left, extension.left);
            let top = Math.min(bounds.top, extension.top);

            bounds.width = Math.max(bounds.left + bounds.width, extension.left + extension.width) - left;
            bounds.height = Math.max(bounds.top + bounds.height, extension.top + extension.height) - top;
            bounds.left = left;
            bounds.top = top;

            return bounds;
        }

        private drawOrnamentText(x: number, y: number, text: string, style: ITablatureTextStyle, direction: OffBarDirection): fabric.IText {
            let originY = direction == OffBarDirection.Top ? "bottom" : "top";
            return this.drawText(text, x, y, "center", originY, style);
        }

        private drawText(text: string, x: number, y: number, originX: string, originY: string, options?: fabric.IITextOptions, addToCanvas: boolean = true): fabric.IText {
            let textElement = new fabric.Text(text, options);
            textElement.originX = originX;
            textElement.originY = originY;
            textElement.left = x;
            textElement.top = y;

            if (addToCanvas)
                this.canvas.add(textElement);

            return textElement;
        }

        drawTitle(title: string, x: number, y: number): IBoundingBox {
            return this.drawText(title, x, y, "center", "top", this.style.title).getBoundingRect();
        }

        callbackWith(result: any) {
            __callbackObject.callback(result);
        }

        drawArtificialHarmonicText(x: number, y: number, text: string, direction: OffBarDirection): IBoundingBox {
            return this.drawOrnamentText(x, y, text, this.style.ornaments.artificialHarmonicsText, direction).getBoundingRect();
        }

        private drawGhostNoteParenthese(bounds: IBoundingBox): IBoundingBox {
            let y = bounds.top + bounds.height / 2;
            let leftBounds = this.drawText("(", bounds.left - this.style.note.margin, y, "center", "center", this.style.fretNumber).getBoundingRect();
            let rightBounds = this.drawText(")", bounds.left + bounds.width + this.style.note.margin, y, "center", "center", this.style.fretNumber).getBoundingRect();
            bounds = PrimitiveRenderer.inflateBounds(bounds, leftBounds);
            bounds = PrimitiveRenderer.inflateBounds(bounds, rightBounds);

            return bounds;
        }

        async drawNoteFretting(fretting: string, x: number, y: number, flags: NoteRenderingFlags) {

            let drawArtificialHarmonic = (flags & NoteRenderingFlags.ArtificialHarmonic) === NoteRenderingFlags.ArtificialHarmonic;
            let drawNaturalHarmonic = (flags & NoteRenderingFlags.NaturalHarmonic) === NoteRenderingFlags.NaturalHarmonic;

            let bounds: IBoundingBox;

            if (drawNaturalHarmonic)
                bounds = PrimitiveRenderer.inflateBounds(bounds, (await this.drawNaturalHarmonicAsync(x, y)).getBoundingRect());

            if (drawArtificialHarmonic)
                bounds = PrimitiveRenderer.inflateBounds(bounds, (await this.drawArtificialHarmonicAsync(x, y)).getBoundingRect());

            let drawSpecialNote = async function (this: PrimitiveRenderer, imageFile: string): Promise<IBoundingBox> {
                let group = await this.drawSVGFromURLAsync(imageFile, x, y,
                    group => {
                        group.scaleY = this.getScale();
                        group.originX = "center";
                        group.originY = "center";
                    });

                return group.getBoundingRect();
            }

            switch (fretting) {
                case "dead":
                    bounds = PrimitiveRenderer.inflateBounds(bounds, (await drawSpecialNote.call(this, ResourceManager.getTablatureResource("dead_note.svg"))));
                    break;
                case "asChord":
                    bounds = PrimitiveRenderer.inflateBounds(bounds, (await drawSpecialNote.call(this, ResourceManager.getTablatureResource("play_to_chord_mark.svg"))));
                    break;
                default:

                    let text = this.drawText(fretting, x, y, "center", "center", this.style.fretNumber);

                    if ((drawArtificialHarmonic || drawNaturalHarmonic) && fretting.length > 1)
                        text.scale(0.8);

                    if (drawArtificialHarmonic)
                        text.setColor("#FFFFFF");

                    bounds = PrimitiveRenderer.inflateBounds(bounds, text.getBoundingRect());

                    break;
            }

            bounds = this.drawAdditionalForNote(bounds, flags);
            this.callbackWith(bounds);
        }

        private drawAdditionalForNote(bounds: IBoundingBox, flags: NoteRenderingFlags): IBoundingBox {

            if ((flags & NoteRenderingFlags.Ghost) === NoteRenderingFlags.Ghost)
                bounds = this.drawGhostNoteParenthese(bounds);

            return bounds;
        }

        drawEllipseAroundBounds(bounds: IBoundingBox): IBoundingBox {

            var ellipse = new fabric.Ellipse({
                left: bounds.left + bounds.width / 2,
                top: bounds.top + bounds.height / 2,
                rx: bounds.width / Math.SQRT2 - this.style.note.longNoteEllipsePadding,
                ry: bounds.height / Math.SQRT2 - this.style.note.longNoteEllipsePadding * bounds.height / bounds.width,
                originX: "center",
                originY: "center",
                stroke: "black",
                fill: ""
            });

            this.canvas.add(ellipse);

            return ellipse.getBoundingRect();
        }

        private drawNaturalHarmonicAsync(x: number, y: number): Promise<fabric.IPathGroup> {
            let imageFile = ResourceManager.getTablatureResource("natural_harmonic.svg");
            return this.drawSVGFromURLAsync(imageFile, x, y, group => {
                group.originX = "center";
                group.originY = "center";
            });
        }


        private drawArtificialHarmonicAsync(x: number, y: number): Promise<fabric.IPathGroup> {
            let imageFile = ResourceManager.getTablatureResource("artificial_harmonic.svg");
            return this.drawSVGFromURLAsync(imageFile, x, y, group => {
                group.originX = "center";
                group.originY = "center";
            });
        }


        drawLyrics(lyrics: string, x: number, y: number): IBoundingBox {
            return this.drawText(lyrics, x, y, "left", "top", this.style.lyrics).getBoundingRect();
        }

        measureLyrics(lyrics: string): IBoundingBox {
            let textElement = new fabric.Text(lyrics, this.style.lyrics);
            this.canvas.add(textElement);       // for now we'll just add and remove it
            // later we should refactor it so all elements are created at once, and re-arranged later
            let bounds = textElement.getBoundingRect();
            this.canvas.remove(textElement);
            return bounds;
        }

        drawTuplet(tuplet: number, x: number, y: number): IBoundingBox {
            return this.drawText(Smufl.GetTupletNumber(tuplet), x, y, "center", "center", this.style.note.tuplet).getBoundingRect();
        }


        private drawLine(x1: number, y1: number, x2: number, y2: number): fabric.ILine {
            let line = new fabric.Line([x1, y1, x2, y2], {
                stroke: "black",
            });
            this.canvas.add(line);
            return line;
        }

        drawHorizontalBarLine(x: number, y: number, length: number) {
            this.drawLine(x, y, x + length, y);
        }

        async drawBarLine(barLine: BarLine, x: number, y: number) {
            let imageFile: string;
            switch (barLine) {
                case BarLine.Standard:
                    imageFile = ResourceManager.getTablatureResource("barline_standard.svg"); break;
                case BarLine.BeginAndEndRepeat:
                    imageFile = ResourceManager.getTablatureResource("barline_begin_and_end_repeat.svg"); break;
                case BarLine.BeginRepeat:
                    imageFile = ResourceManager.getTablatureResource("barline_begin_repeat.svg"); break;
                case BarLine.BeginRepeatAndEnd:
                    imageFile = ResourceManager.getTablatureResource("barline_begin_repeat_and_end.svg"); break;
                case BarLine.Double:
                    imageFile = ResourceManager.getTablatureResource("barline_double.svg"); break;
                case BarLine.End:
                    imageFile = ResourceManager.getTablatureResource("barline_end.svg"); break;
                case BarLine.EndRepeat:
                    imageFile = ResourceManager.getTablatureResource("barline_end_repeat.svg"); break;
            }

            await this.drawSVGFromURLAsync(imageFile, x, y, group => {
                group.scaleToHeight(this.style.bar.lineHeight * 5);
            });
        }

        drawStem(x: number, yFrom: number, yTo: number) {
            this.drawLine(x, yFrom, x, yTo);
        }

        private loadSVGFromURLAsync(url: string): Promise<fabric.IPathGroup> {
            return new Promise<fabric.IPathGroup>((resolve, reject) => {
                fabric.loadSVGFromURL(url, (results, options) => {
                    let group = fabric.util.groupSVGElements(results, options);
                    resolve(group);
                });
            });
        }

        private async drawSVGFromURLAsync(url: string, x: number, y: number, postProcessing?: (group: fabric.IPathGroup) => void): Promise<fabric.IPathGroup> {

            let group = await this.loadSVGFromURLAsync(url);
            group.left = x;
            group.top = y;

            if (postProcessing != null)
                postProcessing(group);

            this.canvas.add(group);

            return group;
        }

        private async drawFlagHead(x: number, y: number, direction: OffBarDirection): Promise<fabric.IPathGroup> {
            let flagFile = ResourceManager.getTablatureResource("note_flag_head.svg");

            let group = await this.drawSVGFromURLAsync(flagFile, x, y, group => {
                group.originX = "left";
                group.originY = "center";
                group.scale(this.getScale());
                if (direction == OffBarDirection.Bottom)
                    group.flipY = true;
            });

            return group;
        }

        async drawFlag(noteValue: BaseNoteValue, x: number, y: number, direction: OffBarDirection) {
            if (noteValue > BaseNoteValue.Eighth)
                return;

            // draw flag bodies
            if (noteValue < BaseNoteValue.Eighth) {

                let flagFile = ResourceManager.getTablatureResource("note_flag_body.svg");

                let group = await this.loadSVGFromURLAsync(flagFile);

                group.left = x;
                group.originX = "left";
                group.originY = "center";
                group.scale(this.getScale());

                let bounds = group.getBoundingRect();

                if (direction == OffBarDirection.Bottom)
                    group.flipY = true;

                for (let i = noteValue; i < BaseNoteValue.Eighth; ++i) {
                    if (i === noteValue) {
                        group.top = y;
                        this.canvas.add(group);
                    }
                    else {
                        group.clone((result: fabric.IPathGroup) => {
                            result.top = y;
                            this.canvas.add(result);
                        });
                    }

                    if (direction == OffBarDirection.Bottom)
                        y -= 6;
                    else
                        y += 6;
                }

            }

            // draw flag head
            {
                let flagFile = ResourceManager.getTablatureResource("note_flag_head.svg");

                let group = await this.drawSVGFromURLAsync(flagFile, x, y, group => {
                    group.originX = "left";
                    group.originY = "center";
                    group.scale(this.getScale());
                    if (direction == OffBarDirection.Bottom)
                        group.flipY = true;
                });

                this.callbackWith(group.getBoundingRect());
            }
        }

        drawBeam(x1: number, y1: number, x2: number, y2: number): IBoundingBox {
            let halfThickness = this.style.bar.beamThickness / 2;
            let points = [
                { x: x1, y: y1 - halfThickness },
                { x: x2, y: y2 - halfThickness },
                { x: x2, y: y2 + halfThickness },
                { x: x1, y: y1 + halfThickness }
            ];
            let polygon = new fabric.Polygon(points);
            polygon.fill = "black";
            polygon.stroke = "black";
            this.canvas.add(polygon);

            return polygon.getBoundingRect();
        }

        drawNoteValueAugment(augment: NoteValueAugment, x: number, y: number) {
            for (let i = 0; i < augment; ++i) {
                let dot = new fabric.Circle({
                    radius: this.style.note.dot.radius,
                    left: x,
                    top: y,
                    originX: "left",
                    originY: "center",
                    stroke: "",
                    fill: "black"
                });
                this.canvas.add(dot);

                x += this.style.note.dot.radius * 2 + this.style.note.dot.spacing;
            }
        }

        private getRestImage(noteValue: BaseNoteValue): string {
            switch (noteValue) {
                case BaseNoteValue.Large:
                case BaseNoteValue.Long:
                case BaseNoteValue.Double:
                case BaseNoteValue.Whole:
                case BaseNoteValue.Half:
                    return ResourceManager.getTablatureResource("rest_2.svg");
                case BaseNoteValue.Quater:
                    return ResourceManager.getTablatureResource("rest_4.svg");
                case BaseNoteValue.Eighth:
                    return ResourceManager.getTablatureResource("rest_8.svg");
                case BaseNoteValue.Sixteenth:
                    return ResourceManager.getTablatureResource("rest_16.svg");
                case BaseNoteValue.ThirtySecond:
                    return ResourceManager.getTablatureResource("rest_32.svg");
                case BaseNoteValue.SixtyFourth:
                    return ResourceManager.getTablatureResource("rest_64.svg");
                case BaseNoteValue.HundredTwentyEighth:
                    return ResourceManager.getTablatureResource("rest_128.svg");
                case BaseNoteValue.TwoHundredFiftySixth:
                    return ResourceManager.getTablatureResource("rest_256.svg");
                default:
                    return null;
            }
        }

        async measureRest(noteValue: BaseNoteValue) {
            let group = await this.loadSVGFromURLAsync(this.getRestImage(noteValue));

            group.originX = "center";
            group.originY = "center";
            group.scale(this.getScale());
            this.callbackWith(group.getBoundingRect());
        }

        async drawRest(noteValue: BaseNoteValue, x: number, y: number) {
            let group = await this.drawSVGFromURLAsync(this.getRestImage(noteValue), x, y, group => {
                group.originX = "center";
                group.originY = "center";
                group.scale(this.getScale());
            });
            this.callbackWith(group.getBoundingRect());
        }

        drawConnectionInstruction(x: number, y: number, instruction: string, direction: OffBarDirection): IBoundingBox {
            return this.drawOrnamentText(x, y, instruction, this.style.ornaments.connectionInstructionText, direction).getBoundingRect();
        }

        async drawTie(x0: number, x1: number, y: number, direction: OffBarDirection) {
            let imageFile = ResourceManager.getTablatureResource("tie.svg");
            let group = await this.drawSVGFromURLAsync(imageFile, x0, y, group => {

                group.scaleToWidth(x1 - x0);
                let standardScaleY = this.getScale();
                group.scaleY = Math.max(standardScaleY / 2, Math.min(Math.sqrt(group.scaleY), standardScaleY));
                group.originX = "left";

                if (direction == OffBarDirection.Bottom) {
                    group.originY = "top";
                    group.flipY = true;
                }
                else {
                    group.originY = "bottom";
                }

            });

            this.callbackWith(group.getBoundingRect());
        }

        async drawGliss(x: number, y: number, direction: GlissDirection) {
            let imageFile = ResourceManager.getTablatureResource("gliss.svg");

            let group = await this.drawSVGFromURLAsync(imageFile, x, y,
                group => {

                    switch (direction) {
                        case GlissDirection.FromHigher:
                            group.flipX = true;
                            group.flipY = true;
                            group.originX = "right";
                            group.originY = "bottom";
                            break;
                        case GlissDirection.FromLower:
                            group.flipX = true;
                            group.originX = "right";
                            group.originY = "top";
                            break;
                        case GlissDirection.ToHigher:
                            group.flipY = true;
                            group.originX = "left";
                            group.originY = "bottom";
                            break;
                        case GlissDirection.ToLower:
                            group.originX = "left";
                            group.originY = "top";
                            break;
                    }

                    group.scaleY = this.getScale();
                });

            this.callbackWith(group.getBoundingRect());
        }

        drawRasgueadoText(x: number, y: number, direction: OffBarDirection): IBoundingBox {
            return this.drawOrnamentText(x, y, "rasg.", this.style.ornaments.rasgueadoText, direction).getBoundingRect();
        }


        private async drawInlineBrushlikeTechnique(x: number, y: number, stringSpan: number, technique: string, isUp: Boolean) {
            let imageFile = ResourceManager.getTablatureResource(`${technique}_up_inline_${stringSpan}.svg`);

            let group = await this.drawSVGFromURLAsync(imageFile, x, y, group => {
                group.originX = "center";
                group.originY = "center";

                group.scaleX = 1.2;

                if (!isUp)
                    group.flipY = true;
            });

            this.callbackWith(group.getBoundingRect());
        }

        async drawInlineBrushDown(x: number, y: number, stringSpan: number) {
            await this.drawInlineBrushlikeTechnique(x, y, stringSpan, "brush", false);
        }

        async drawInlineBrushUp(x: number, y: number, stringSpan: number) {
            await this.drawInlineBrushlikeTechnique(x, y, stringSpan, "brush", true);
        }

        async drawInlineArpeggioDown(x: number, y: number, stringSpan: number) {
            await this.drawInlineBrushlikeTechnique(x, y, stringSpan, "arpeggio", false);
        }

        async drawInlineArpeggioUp(x: number, y: number, stringSpan: number) {
            await this.drawInlineBrushlikeTechnique(x, y, stringSpan, "arpeggio", true);
        }

        async drawInlineRasgueado(x: number, y: number, stringSpan: number) {
            await this.drawInlineBrushlikeTechnique(x, y, stringSpan, "arpeggio", false);
        }

        private async drawOrnamentImageFromURL(urlResourceName: string, x: number, y: number, direction: OffBarDirection, postProcessing?: (group: fabric.IPathGroup) => void) {
            let imageFile = ResourceManager.getTablatureResource(urlResourceName);
            let group = await this.drawSVGFromURLAsync(imageFile, x, y, group => {
                group.originX = "center";
                group.originY = direction == OffBarDirection.Top ? "bottom" : "top";

                if (direction == OffBarDirection.Bottom)
                    group.flipY = true;

                if (postProcessing != null)
                    postProcessing(group);
            });

            this.callbackWith(group.getBoundingRect());
        }

        drawPickstrokeDown(x: number, y: number, direction: OffBarDirection) {
            this.drawOrnamentImageFromURL("downbow.svg", x, y, direction);
        }

        drawPickstrokeUp(x: number, y: number, direction: OffBarDirection) {
            this.drawOrnamentImageFromURL("upbow.svg", x, y, direction);
        }

        drawAccented(x: number, y: number, direction: OffBarDirection) {
            this.drawOrnamentImageFromURL("accented.svg", x, y, direction);
        }

        drawHeavilyAccented(x: number, y: number, direction: OffBarDirection) {
            this.drawOrnamentImageFromURL("heavily_accented.svg", x, y, direction);
        }

        drawFermata(x: number, y: number, direction: OffBarDirection) {
            this.drawOrnamentImageFromURL("fermata.svg", x, y, direction);
        }

        drawStaccato(x: number, y: number, direction: OffBarDirection) {
            this.drawOrnamentImageFromURL("staccato.svg", x, y, direction);
        }

        drawTenuto(x: number, y: number, direction: OffBarDirection) {
            this.drawOrnamentImageFromURL("tenuto.svg", x, y, direction);
        }

        drawTrill(x: number, y: number, direction: OffBarDirection) {
            this.drawOrnamentImageFromURL("trill.svg", x, y, direction);
        }

        drawTremolo(x: number, y: number, direction: OffBarDirection) {
            this.drawOrnamentImageFromURL("tremolo.svg", x, y, direction);
        }

        drawBrushUp(x: number, y: number, direction: OffBarDirection) {
            this.drawOrnamentImageFromURL("brush_up.svg", x, y, direction);
        }

        drawBrushDown(x: number, y: number, direction: OffBarDirection) {
            this.drawOrnamentImageFromURL("brush_down.svg", x, y, direction);
        }

        drawArpeggioUp(x: number, y: number, direction: OffBarDirection) {
            this.drawOrnamentImageFromURL("arpeggio_up.svg", x, y, direction);
        }

        drawArpeggioDown(x: number, y: number, direction: OffBarDirection) {
            this.drawOrnamentImageFromURL("arpeggio_down.svg", x, y, direction);
        }

        drawTimeSignature(x: number, y: number, beats: number, timeValue: number): IBoundingBox {
            let textValue: string;
            if (beats === 4 && timeValue === 4) {
                textValue = Smufl.GetCharacter("timeSigCommon");  // common time
            }
            else {
                textValue = `${Smufl.GetTimeSignatureNumber(beats)}\n${Smufl.GetTimeSignatureNumber(timeValue)}`;
            }

            let text = this.drawText(textValue, x, y + this.style.bar.timeSignatureOffset, "center", "center", this.style.bar.timeSignature);
            text.textAlign = "center";
            return text.getBoundingRect();
        }

        async drawTabHeader(x: number, y: number) {

            let imageFile = ResourceManager.getTablatureResource("tab_header.svg");

            let group = await this.drawSVGFromURLAsync(imageFile, x, y, group => {
                group.originX = "left";
                group.originY = "center";
            });

            this.callbackWith(group.getBoundingRect());
        }

        drawTranspositionText(x: number, y: number, key: string): IBoundingBox {
            let textValue = `transpose to ${key}`;
            let text = this.drawText(textValue, x, y, "left", "bottom", this.style.documentState.transposition);
            return text.getBoundingRect();
        }

        drawTempoSignature(x: number, y: number, noteValue: BaseNoteValue, beats: number): IBoundingBox {
            let textValue = `${Smufl.GetNoteValue(noteValue)} = ${beats}`;
            let text = this.drawText(textValue, x, y, "left", "bottom", this.style.documentState.tempo);
            return text.getBoundingRect();
        }

        drawSection(x: number, y: number, section: string): IBoundingBox {
            let text = this.drawText(section, x, y, "left", "bottom", this.style.documentState.section);
            let bounds = text.getBoundingRect();
            const padding = this.style.documentState.sectionTextPadding;
            let rect = new fabric.Rect({
                left: bounds.left - padding,
                top: bounds.top - padding,
                width: bounds.width + padding * 2,
                height: bounds.height + padding * 2,
                stroke: "black",
                fill: "",
                strokeWidth: 1
            });
            this.canvas.add(rect);
            return rect.getBoundingRect();
        }

        private drawAlternativeEndingText(x: number, y: number, alternationText: string) {
            this.drawText(alternationText,
                x + this.style.documentState.alternativeEndingTextPadding,
                y,
                "left",
                "top",
                this.style.documentState.alternativeEndingText);
        }

        drawStartAlternation(x0: number, x1: number, y0: number, y1: number, alternationText: string): IBoundingBox {

            y1 -= this.style.documentState.alternativeEndingHeight;

            this.drawAlternativeEndingText(x0 + this.style.documentState.alternativeEndingTextPadding,
                y1 + this.style.documentState.alternativeEndingTextPadding,
                alternationText);

            let polyline = new fabric.Polyline([
                { x: x0, y: y0 },
                { x: x0, y: y1 },
                { x: x1, y: y1 }
            ], {
                    stroke: "black",
                    fill: "",
                    strokeWidth: 1
                });

            this.canvas.add(polyline);
            return polyline.getBoundingRect();
        }

        drawStartAndEndAlternation(x0: number, x1: number, y0: number, y1: number, alternationText: string): IBoundingBox {

            y1 -= this.style.documentState.alternativeEndingHeight;
            x1 -= this.style.documentState.endAlternativeEndingRightMargin;

            this.drawAlternativeEndingText(x0 + this.style.documentState.alternativeEndingTextPadding,
                y1 + this.style.documentState.alternativeEndingTextPadding,
                alternationText);

            let polyline = new fabric.Polyline([
                { x: x0, y: y0 },
                { x: x0, y: y1 },
                { x: x1, y: y1 },
                { x: x1, y: y0 },
            ], {
                    stroke: "black",
                    fill: "",
                    strokeWidth: 1
                });

            this.canvas.add(polyline);
            return polyline.getBoundingRect();
        }

        drawAlternationLine(x0: number, x1: number, y1: number): IBoundingBox {

            y1 -= this.style.documentState.alternativeEndingHeight;

            let line = new fabric.Line([x0, y1, x1, y1], {
                stroke: "black",
                fill: "",
                strokeWidth: 1
            });

            this.canvas.add(line);
            return line.getBoundingRect();
        }

        drawEndAlternation(x0: number, x1: number, y0: number, y1: number): IBoundingBox {

            y1 -= this.style.documentState.alternativeEndingHeight;
            x1 -= this.style.documentState.endAlternativeEndingRightMargin;

            let polyline = new fabric.Polyline([
                { x: x0, y: y1 },
                { x: x1, y: y1 },
                { x: x1, y: y0 }
            ], {
                    stroke: "black",
                    fill: "",
                    strokeWidth: 1
                });

            this.canvas.add(polyline);
            return polyline.getBoundingRect();
        }

        debugDrawHeightMap(points: { x: number, y: number }[]) {
            let polyline = new fabric.Polyline(points, {
                stroke: "green",
                fill: ""
            });

            this.canvas.add(polyline);
        }

        private drawChordSpecialStringTokens(x: number, y: number, fingering: Core.MusicTheory.IExplicitChordFingeringNote[], bounds: IBoundingBox) {
            enum SpecialStringTokens {
                None,
                Open,
                Skip,
            };

            let tokens = fingering.map(f => {
                if (f.fret === -1)
                    return SpecialStringTokens.Skip;
                return f.fret === 0 ? SpecialStringTokens.Open : SpecialStringTokens.None;
            });

            if (!tokens.reduce((result, t) => (t != SpecialStringTokens.None) || result, false)) {
                return;
            }

            let cellX = x;
            let textStyle: any = this.getSmuflTextStyle(32);
            y -= this.style.chordDiagram.specialStringTokenPadding.bottom;

            tokens.forEach(t => {

                if (t != SpecialStringTokens.None) {
                    let textContent: string;
                    switch (t) {
                        case SpecialStringTokens.Open: textContent = Smufl.GetCharacter('fretboardO'); break;
                        case SpecialStringTokens.Skip: textContent = Smufl.GetCharacter('fretboardX'); break;
                    }

                    let text = this.drawText(textContent, cellX, y, "center", "bottom", textStyle);
                    PrimitiveRenderer.inflateBounds(bounds, text.getBoundingRect());
                }

                cellX += this.style.chordDiagram.cellWidth;
            });

            bounds.top -= this.style.chordDiagram.elementSpacing + this.style.chordDiagram.specialStringTokenPadding.top;
        }

        private drawChordDiagramGrid(x: number, y: number, minFret: number, maxFret: number, bounds: IBoundingBox) {

            let width = this.style.chordDiagram.cellWidth * (this.style.stringCount - 1);

            let fretY = y;

            let group = new fabric.Group();
            for (let fret = maxFret + 1; fret >= minFret; --fret) {

                if (fret == 1) {
                    let rect = new fabric.Rect({
                        left: x,
                        top: fretY - this.style.chordDiagram.nutThickness + this.style.chordDiagram.gridThickness,
                        width: width,
                        height: this.style.chordDiagram.nutThickness,
                        fill: "black",
                        stroke: "black"
                    });

                    group.addWithUpdate(rect);
                }
                else {
                    let line = new fabric.Line([x, fretY, x + width, fretY], {
                        stroke: "black",
                        fill: "",
                        strokeWidth: this.style.chordDiagram.gridThickness
                    });

                    group.addWithUpdate(line);

                    if (fret > minFret)
                        fretY -= this.style.chordDiagram.cellHeight;
                }

            }

            let stringX = x;
            for (let stringIndex = 0; stringIndex < this.style.stringCount; ++stringIndex) {
                let line = new fabric.Line([stringX, y, stringX, fretY], {
                    stroke: "black",
                    fill: "",
                    strokeWidth: this.style.chordDiagram.gridThickness
                });
                group.addWithUpdate(line);
                stringX += this.style.chordDiagram.cellWidth;
            }

            this.canvas.add(group);
            PrimitiveRenderer.inflateBounds(bounds, group.getBoundingRect());

            bounds.top -= this.style.chordDiagram.elementSpacing;
        }

        private drawChordSingleFingering(x: number, y: number) {
            let circle = new fabric.Circle({
                radius: this.style.chordDiagram.fingeringTokenRadius,
                left: x,
                top: y,
                originX: "center",
                originY: "center",
                fill: "black",
            });

            this.canvas.add(circle);
        }

        private drawChordGetStringX(x: number, stringIndex: number) {
            return x + stringIndex * this.style.chordDiagram.cellWidth + this.style.chordDiagram.gridThickness / 2;
        }

        private drawChordBarre(barredStrings: number[], x: number, y: number) {

            let from = this.drawChordGetStringX(x, barredStrings[0]);
            let to = this.drawChordGetStringX(x, barredStrings[barredStrings.length - 1]);

            if (barredStrings.length === 1 || from === to) {
                this.drawChordSingleFingering(from, y);
                return;
            }
            this.drawChordSingleFingering(from, y);
            this.drawChordSingleFingering(to, y);

            let rect = new fabric.Rect({
                left: from,
                width: to - from,
                top: y,
                height: this.style.chordDiagram.fingeringTokenRadius * 2,
                originX: "left",
                originY: "center",
                fill: "black",
                stroke: ""
            });
            this.canvas.add(rect);
        }


        private drawChordFingering(x: number, y: number, minFret: number, maxFret: number, notes: Core.MusicTheory.IExplicitChordFingeringNote[], bounds: IBoundingBox) {

            let getStringX = (stringIndex: number) => x + stringIndex * this.style.chordDiagram.cellWidth + this.style.chordDiagram.gridThickness / 2;

            let fretY = y - this.style.chordDiagram.cellHeight / 2;
            for (let fret = maxFret; fret >= minFret; --fret) {

                let finger = <number>null;
                let barredStrings = new Array<number>();

                for (let stringIndex = 0; stringIndex < notes.length; ++stringIndex) {
                    let note = notes[stringIndex];

                    if (note.fret > fret)
                        continue;
                    else if (note.fret < fret) {

                        // break barre if it's blocking a lower fret note
                        if (barredStrings.length > 0) {
                            this.drawChordBarre(barredStrings, x, fretY);
                            barredStrings.length = 0;
                        }

                        continue;
                    }

                    if (note.finger === null && finger === null) {
                        this.drawChordSingleFingering(getStringX(stringIndex), fretY);
                        continue;
                    }

                    if (note.finger !== null) {
                        if (finger === null)
                            barredStrings.push(stringIndex);
                        else {
                            if (finger === note.finger)
                                barredStrings.push(stringIndex);
                            else {
                                this.drawChordBarre(barredStrings, x, fretY);
                                barredStrings.length = 0;
                                barredStrings.push(stringIndex);
                            }
                        }
                        finger = note.finger;
                    }
                    else {
                        if (finger !== null) {
                            this.drawChordBarre(barredStrings, x, fretY);
                            barredStrings.length = 0;
                            finger = null;
                        }
                        else {
                            this.drawChordSingleFingering(getStringX(stringIndex), fretY);
                        }
                    }
                }

                if (finger !== null)
                    this.drawChordBarre(barredStrings, x, fretY);

                fretY -= this.style.chordDiagram.cellHeight;
            }
        }

        private drawChordFingerIndices(x: number, y: number, fingering: Core.MusicTheory.IExplicitChordFingeringNote[], bounds: IBoundingBox) {

            const finger_skip = 0;
            const finger_unknown = -1;

            let fingers = fingering.map(f => {
                if (f.fret <= 0)
                    return finger_skip;
                return f.finger === null ? finger_unknown : f.finger;
            });

            if (fingers.reduce((count, f) => (f > 0) ? count + 1 : count, 0) === 0) {
                return;
            }

            let cellX = x;

            fingers.forEach(f => {
                let textContent: string;
                if (f == finger_skip) textContent = "-";
                else if (f == finger_unknown) textContent = "?";
                else textContent = f.toString();

                let text = this.drawText(textContent, cellX, y, "center", "bottom", this.style.chordDiagram.fingeringText);
                PrimitiveRenderer.inflateBounds(bounds, text.getBoundingRect());

                cellX += this.style.chordDiagram.cellWidth;
            });

            bounds.top -= this.style.chordDiagram.elementSpacing;

        }

        private drawChordFretOffset(x: number, y: number, fret: number, bounds: IBoundingBox) {
            let text = this.drawText(`${fret}fr`, x, y, "left", "center", this.style.chordDiagram.fretText);
            PrimitiveRenderer.inflateBounds(bounds, text.getBoundingRect());
        }

        private static explicifyChordFingering(fingering: Core.MusicTheory.IChordFingeringNote[]): Core.MusicTheory.IExplicitChordFingeringNote[] {
            return fingering.map(f => {
                if (f === "x")
                    return { fret: -1 };
                if (typeof f === "number")
                    return { fret: f };
                return f;
            });
        }

        drawChord(x: number, y: number, name: string, fingering: Core.MusicTheory.IChordFingeringNote[]): IBoundingBox {

            let bounds: IBoundingBox = { left: x, top: y, width: 0, height: 0 };

            if (fingering.length > 0) {

                let explicitFingering = PrimitiveRenderer.explicifyChordFingering(fingering);

                this.drawChordFingerIndices(x, y, explicitFingering, bounds);

                let frets = explicitFingering.map<number>(f => f.fret).filter(f => f > 0);

                if (frets.length === 0) {
                    this.drawChordDiagramGrid(bounds.left, bounds.top, 1, 3, bounds);
                    this.drawChordSpecialStringTokens(bounds.left, bounds.top, explicitFingering, bounds);
                    return bounds;
                }

                let minFret = Math.min(...frets);
                let maxFret = Math.max(...frets);
                let fretSpan = maxFret - minFret + 1;

                if (fretSpan < 3) {
                    if (maxFret < 3) {
                        maxFret = 3;
                        minFret = maxFret - 2;
                    }
                    else if (maxFret < 4)
                        minFret = 1;
                    else
                        minFret = maxFret - 2;
                }

                let diagramX = bounds.left;
                let diagramY = bounds.top;
                this.drawChordDiagramGrid(diagramX, diagramY, minFret, maxFret, bounds);

                this.drawChordFingering(diagramX, diagramY, minFret, maxFret, explicitFingering, bounds);

                if (minFret !== 1) {
                    this.drawChordFretOffset(bounds.left + bounds.width + this.style.chordDiagram.elementSpacing,
                        bounds.top + this.style.chordDiagram.cellHeight / 2,
                        minFret,
                        bounds);
                }

                this.drawChordSpecialStringTokens(bounds.left, bounds.top, explicitFingering, bounds);
            }


            let text = this.drawText(name,
                bounds.left + this.style.chordDiagram.cellWidth * (this.style.stringCount - 1) / 2,
                bounds.top,
                "center",
                "bottom",
                this.style.chordDiagram.nameText);

            PrimitiveRenderer.inflateBounds(bounds, text.getBoundingRect());

            return bounds;
        }


    }
}