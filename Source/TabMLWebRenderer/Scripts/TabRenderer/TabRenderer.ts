import StaticCanvas = fabric.IStaticCanvas;

class TabRenderer {
    private canvas: StaticCanvas;

    constructor(canvas: HTMLCanvasElement) {
        this.canvas = new fabric.Canvas(canvas);
    }

    drawTitle(title: string) {
        var comicSansText = new fabric.Text(title,
            {
                fontFamily: 'Comic Sans'
            });
        this.canvas.add(comicSansText);
    }
}
