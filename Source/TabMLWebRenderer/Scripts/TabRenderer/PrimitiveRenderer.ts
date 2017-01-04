import BarLine = Core.MusicTheory.BarLine;
import BaseNoteValue = Core.MusicTheory.BaseNoteValue;
import OffBarDirection = Core.MusicTheory.OffBarDirection;
import NoteValueAugment = Core.MusicTheory.NoteValueAugment;
import GlissDirection = Core.MusicTheory.GlissDirection;
import NoteRenderingFlags = TR.NoteRenderingFlags;


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

        private drawText(text: string, x: number, y: number, originX: string, originY: string, options?: fabric.IITextOptions): fabric.IText {
            let textElement = new fabric.Text(text, options);
            textElement.originX = originX;
            textElement.originY = originY;
            textElement.left = x;
            textElement.top = y;
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

            if ((flags & NoteRenderingFlags.HalfOrLonger) === NoteRenderingFlags.HalfOrLonger && this.style.note.circleOnLongNotes)
                bounds = this.drawCircleAroundLongNote(bounds);

            if ((flags & NoteRenderingFlags.Ghost) === NoteRenderingFlags.Ghost)
                bounds = this.drawGhostNoteParenthese(bounds);

            return bounds;
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

        private drawCircleAroundLongNote(bounds: IBoundingBox): IBoundingBox {
            let radius = Math.max(bounds.width, bounds.height) / 2 + this.style.note.longNoteCirclePadding;
            let circle = new fabric.Circle({
                radius: radius,
                left: bounds.left + bounds.width / 2,
                top: bounds.top + bounds.height / 2,
                originX: "center",
                originY: "center",
                stroke: "black",
                fill: ""
            });
            this.canvas.add(circle);

            return PrimitiveRenderer.inflateBounds(bounds, circle.getBoundingRect());
        }

        drawLyrics(lyrics: string, x: number, y: number): IBoundingBox {
            return this.drawText(lyrics, x, y, "left", "top", this.style.lyrics).getBoundingRect();
        }

        measureLyrics(lyrics: string): IBoundingBox {
            let textElement = new fabric.Text(lyrics, this.style.lyrics);

            return textElement.getBoundingRect();
        }

        drawTuplet(tuplet: string, x: number, y: number): IBoundingBox {
            return this.drawText(tuplet, x, y, "center", "center", this.style.note.tuplet).getBoundingRect();
        }


        private drawLine(x1: number, y1: number, x2: number, y2: number): fabric.ILine {
            let line = new fabric.Line([x1, y1, x2, y2]);
            line.stroke = "black";
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
            await this.drawSVGFromURLAsync(this.getRestImage(noteValue), x, y, group => {
                group.originX = "center";
                group.originY = "center";
                group.scale(this.getScale());
            });
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

        private async drawOrnamentImageFromURL(urlResourceName: string, x: number, y: number, direction: OffBarDirection, postProcessing?: (group: fabric.IPathGroup) => void) {
            let imageFile = ResourceManager.getTablatureResource(urlResourceName);
            let group = await this.drawSVGFromURLAsync(imageFile, x, y, group => {
                group.originX = "center";
                group.originY = direction == OffBarDirection.Top ? "bottom" : "top";

                if(direction == OffBarDirection.Bottom)
                    group.flipY = true;

                if(postProcessing!=null)
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

        debugDrawHeightMap(points: { x:number, y:number }[]) {
            let polyline = new fabric.Polyline(points, {
                stroke: "green",
                fill: ""
            });

            this.canvas.add(polyline);
        }

    }
}