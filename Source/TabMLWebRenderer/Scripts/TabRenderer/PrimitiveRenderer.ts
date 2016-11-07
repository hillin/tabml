class PrimitiveRenderer {
    private canvas: fabric.IStaticCanvas;
    private ITablatureStyle: ITablatureStyle;

    constructor(canvas: fabric.IStaticCanvas, style: ITablatureStyle) {
        this.canvas = canvas;
        this.ITablatureStyle = style;
    }

    drawTitle(title: string, x: number, y: number) {
        var text = new fabric.Text(title, this.ITablatureStyle.title);
        text.left = x;
        text.top = y;
        text.originX = "center";
        text.originY = "top";
        this.canvas.add(text);
    }

    drawFretNumber(fretNumber: string, x: number, y: number) {
        var text = new fabric.Text(fretNumber, this.ITablatureStyle.title);
        text.left = x;
        text.top = y;
        text.originX = "center";
        text.originY = "center";
        this.canvas.add(text);
    }

    drawLyrics(lyrics: string, x: number, y: number) {
        var text = new fabric.Text(lyrics, this.ITablatureStyle.lyrics);
        text.left = x;
        text.top = y;
        text.originX = "left";
        text.originY = "top";
        this.canvas.add(text);
    }

    drawBarLine(x: number, y: number, length: number) {
        var line = new fabric.Line([x, y, x + length, y]);
        line.stroke = "black";
        this.canvas.add(line);
    }
}