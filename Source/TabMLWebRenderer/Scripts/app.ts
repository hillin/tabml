
let tablatureStyle: ITablatureStyle =
    {

        fallback: {
            fontFamily: "Segoe UI",
        },

        page: {
            width: 800,
            height: 1200
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

let renderer: PrimitiveRenderer;

window.onload = () => {
    let canvas = document.getElementById("staff") as HTMLCanvasElement;

    let fabricCanvas = new fabric.StaticCanvas(canvas, tablatureStyle.page);
    fabricCanvas.backgroundColor = "white";

    renderer = new PrimitiveRenderer(fabricCanvas, tablatureStyle);

};