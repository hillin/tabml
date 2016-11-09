
let tablatureStyle: TR.ITablatureStyle =
    {

        fallback: {
            fontFamily: "Segoe UI",
        },

        page: {
            width: 800,
            height: 1200
        },

        bar: {
            lineHeight: 12
        },

        title: {
            fontSize: 32,
            fontFamily: "Felix Titling"
        },

        fretNumber: {
            fontSize: 12,
            fontFamily: "Segoe UI"
        },

        lyrics: {
            fontSize: 13,
            fontFamily: "Times New Roman"
        }

    };

let renderer: TR.PrimitiveRenderer;

window.onload = () => {
    let canvas = document.getElementById("staff") as HTMLCanvasElement;

    let fabricCanvas = new fabric.StaticCanvas(canvas, tablatureStyle.page);
    fabricCanvas.backgroundColor = "white";

    renderer = new TR.PrimitiveRenderer(fabricCanvas, tablatureStyle);
    //renderer.drawTitle("test!!!", 400, 100);
    //renderer.drawBarLine(Core.MusicTheory.BarLine.BeginAndEndRepeat, 100, 100);
};