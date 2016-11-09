namespace TR {

    export class TabRenderer {
        private canvas: fabric.IStaticCanvas;
        private ITablatureStyle: ITablatureStyle;

        constructor(canvas: HTMLCanvasElement, ITablatureStyle: ITablatureStyle) {
            this.ITablatureStyle = ITablatureStyle;
            this.canvas = new fabric.StaticCanvas(canvas);
            this.canvas.setDimensions(this.ITablatureStyle.page);
        }
    }
}