import BarLine = Core.MusicTheory.BarLine;
import BaseNoteValue = Core.MusicTheory.BaseNoteValue;
import OffBarDirection = Core.MusicTheory.OffBarDirection;
import NoteValueAugment = Core.MusicTheory.NoteValueAugment;
import GlissDirection = Core.MusicTheory.GlissDirection;

interface CallbackObject {
    callback(result: any): void;
}

declare var __callbackObject: CallbackObject;

namespace TR {

    export interface BoundingBox {
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

        private getScale() : number {
            return this.style.bar.lineHeight / ResourceManager.referenceBarSpacing;
        }

        private drawText(text: string, x:number, y: number, originX: string, originY: string, options?: fabric.IITextOptions) : BoundingBox
        {
            let textElement = new fabric.Text(text, options);
            textElement.originX = originX;
            textElement.originY = originY;
            textElement.left = x;
            textElement.top = y;
            this.canvas.add(textElement);

            return textElement.getBoundingRect();
        }

        drawTitle(title: string, x: number, y: number) : BoundingBox {
             return this.drawText(title, x, y, "center", "top", this.style.title);
        }

        callbackWith(result: any) {
            __callbackObject.callback(result);
        }

        private drawSpecialFretting(imageFile: string, x: number, y: number, isHalfOrLonger: boolean) {
            this.drawSVGFromURL(imageFile, x, y, 
            group => {
                group.scaleY = this.getScale();
                group.originX = "center";
                group.originY = "center";
            },
            group => {
                let bounds = group.getBoundingRect();

                if (isHalfOrLonger && this.style.note.circleOnLongNotes)
                    this.callbackWith(this.drawCircleAroundLongNote(x, y, bounds));
                else
                    this.callbackWith(bounds);
            });
        }

        drawFretNumber(fretNumber: string, x: number, y: number, isHalfOrLonger: boolean) : BoundingBox {
            let bounds = this.drawText(fretNumber, x, y, "center", "center", this.style.fretNumber);

            if (isHalfOrLonger && this.style.note.circleOnLongNotes) {
                return this.drawCircleAroundLongNote(x, y, bounds);
            }

            return bounds;
        }

        private drawCircleAroundLongNote(x: number, y: number, bounds: BoundingBox) : BoundingBox {
            let radius = Math.max(bounds.width, bounds.height) / 2 + this.style.note.longNoteCirclePadding;
            let circle = new fabric.Circle({
                radius: radius,
                left: x,
                top: y,
                originX: "center",
                originY: "center",
                stroke: "black",
                fill: ""
            });
            this.canvas.add(circle);

            return circle.getBoundingRect();
        }

        /*async*/ drawDeadNote(x: number, y: number, isHalfOrLonger: boolean) {
            this.drawSpecialFretting(ResourceManager.getTablatureResource("dead_note.svg"),
                x, y, isHalfOrLonger);
        }

        /*async*/ drawPlayToChordMark(x: number, y: number, isHalfOrLonger: boolean) {
            this.drawSpecialFretting(ResourceManager.getTablatureResource("play_to_chord_mark.svg"),
                x, y, isHalfOrLonger);
        }

        drawLyrics(lyrics: string, x: number, y: number): BoundingBox {
            return this.drawText(lyrics, x, y, "left", "top", this.style.lyrics);
        }

        drawTuplet(tuplet: string, x: number, y: number) : BoundingBox {
            return this.drawText(tuplet, x, y, "center", "center", this.style.note.tuplet);
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

        drawBarLine(barLine: BarLine, x: number, y: number) {
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

            this.drawSVGFromURL(imageFile, x, y, group => {
                group.scaleToHeight(this.style.bar.lineHeight * 5);
            });
        }

        drawStem(x: number, yFrom: number, yTo: number) {
            this.drawLine(x, yFrom, x, yTo);
        }

        private drawSVGFromURL(url: string, x: number, y: number, postProcessing?: (group: fabric.IPathGroup) => void, callback? : (group: fabric.IPathGroup) => void) {
            fabric.loadSVGFromURL(url, (results, options) => {
                let group = fabric.util.groupSVGElements(results, options);
                group.left = x;
                group.top = y;
                if (postProcessing != null)
                    postProcessing(group);

                this.canvas.add(group);

                if(callback!=null)
                    callback(group);
            });
        }

        /*async*/ drawFlag(noteValue: BaseNoteValue, x: number, y: number, direction: OffBarDirection) {
            if (noteValue > BaseNoteValue.Eighth)
                return;

            let flagFile = ResourceManager.getTablatureResource("note_flag.svg");

            fabric.loadSVGFromURL(flagFile, (results, options) => {
                let group = fabric.util.groupSVGElements(results, options);
                group.left = x;
                group.originX = "left";
                group.originY = "center";
                group.scale(this.getScale());

                let bounds = group.getBoundingRect();

                if (direction == OffBarDirection.Bottom)
                    group.flipY = true;

                for (let i = noteValue; i < BaseNoteValue.Quater; ++i) {
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
                    {
                        y -= 6;
                        bounds.top -= 6;
                    }
                    else
                    {
                        y += 6;
                        bounds.top += 6;
                    }

                    this.callbackWith(bounds);
                }
            });
        }

        drawBeam(x1: number, y1: number, x2: number, y2: number) {
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

        private getRestImage(noteValue: BaseNoteValue) : string {
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

        /*async*/ measureRest(noteValue: BaseNoteValue) {
            fabric.loadSVGFromURL(this.getRestImage(noteValue), (results, options) => {
                let group = fabric.util.groupSVGElements(results, options);
                group.originX = "center";
                group.originY = "center";
                group.scale(this.getScale());
                this.callbackWith(group.getBoundingRect());
            });
        }

        drawRest(noteValue: BaseNoteValue, x: number, y: number) {
            this.drawSVGFromURL(this.getRestImage(noteValue), x, y, group => {
                group.originX = "center";
                group.originY = "center";
                group.scale(this.getScale());
            });
        }

        drawTieInstruction(x: number, y: number, instruction: string, direction: OffBarDirection) : BoundingBox {
            let originY = direction == OffBarDirection.Top ? "bottom" : "top";
            return this.drawText(instruction, x, y, "center", originY, this.style.tie.instructionText);
        }

        drawTie(x0: number, x1: number, y: number, direction: OffBarDirection) {
            let imageFile = ResourceManager.getTablatureResource("tie.svg");
            this.drawSVGFromURL(imageFile, x0, y, group => {

                group.scaleToWidth(x1 - x0);
                let standardScaleY = this.getScale();
                group.scaleY = Math.max(standardScaleY/2, Math.min(Math.sqrt(group.scaleY), standardScaleY));
                group.originX = "left";

                if (direction == OffBarDirection.Bottom) {
                    group.originY = "top";
                    group.flipY = true;
                }
                else {
                    group.originY = "bottom";
                }

            });
        }

        /*async*/ drawGliss(x: number, y: number, direction: GlissDirection) {
            let imageFile = ResourceManager.getTablatureResource("gliss.svg");

            this.drawSVGFromURL(imageFile, x, y, 
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
            },
            group => {
                this.callbackWith(group.getBoundingRect());
            });

        }

    }
}