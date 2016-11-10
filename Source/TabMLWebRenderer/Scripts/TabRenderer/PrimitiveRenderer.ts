import BarLine = Core.MusicTheory.BarLine;
import BaseNoteValue = Core.MusicTheory.BaseNoteValue;
import OffBarDirection = Core.MusicTheory.OffBarDirection;

namespace TR {
    export class PrimitiveRenderer {

        private canvas: fabric.IStaticCanvas;
        private style: ITablatureStyle;

        constructor(canvas: fabric.IStaticCanvas, style: ITablatureStyle) {
            this.canvas = canvas;
            this.style = style;
        }

        drawTitle(title: string, x: number, y: number) {
            let text = new fabric.Text(title, this.style.title);
            text.left = x;
            text.top = y;
            text.originX = "center";
            text.originY = "top";
            this.canvas.add(text);
        }

        drawFretNumber(fretNumber: string, x: number, y: number) {
            let text = new fabric.Text(fretNumber, this.style.fretNumber);
            text.left = x;
            text.top = y;
            text.originX = "center";
            text.originY = "center";
            this.canvas.add(text);
        }

        drawLyrics(lyrics: string, x: number, y: number) {
            let text = new fabric.Text(lyrics, this.style.lyrics);
            text.left = x;
            text.top = y;
            text.originX = "left";
            text.originY = "top";
            this.canvas.add(text);
        }

        private drawLine(x1: number, y1: number, x2:number, y2:number): fabric.ILine {
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

            this.drawSVGFromURL(imageFile, x, y, group=>{
                group.scaleToHeight(this.style.bar.lineHeight * 5);
            });
        }

        drawStem(x: number, yFrom: number, yTo: number) {
            this.drawLine(x, yFrom, x, yTo);
        }

        private drawSVGFromURL(url: string, x: number, y: number, callback?: (group: fabric.IPathGroup) => void) {
            fabric.loadSVGFromURL(url, (results, options) => {
                let group = fabric.util.groupSVGElements(results, options);
                group.left = x;
                group.top = y;
                if (callback != null)
                    callback(group);
                this.canvas.add(group);
            });
        }

        drawFlag(noteValue:BaseNoteValue, x: number, y:number, direction:OffBarDirection) {
            // todo
        }

        drawBeam(x1: number, y1: number, x2: number, y2: number) {
            let halfThickness = this.style.bar.beamThickness / 2;
            let points = [
                { x: x1, y: y1 - halfThickness},
                { x: x2, y: y2 - halfThickness},
                { x: x2, y: y2 + halfThickness},
                { x: x1, y: y1 + halfThickness}
            ];
            let polygon = new fabric.Polygon(points);
            polygon.fill = "black";
            this.canvas.add(polygon);
        }
    }
}