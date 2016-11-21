
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
            lineHeight: 12,
            beamThickness: 4,
            beamSpacing: 4
        },

        note: {
            circleOnLongNotes: true,
            longNoteCirclePadding: 1,
            dot: {
                radius: 1.5,
                offset: 3,
                spacing: 2
            },
            flagSpacing: 4,
            tuplet: {
                fontSize: 12,
                fontFamily: "Times New Roman",
                fontStyle: "italic"
            }
        },

        tie: {
            instructionOffset: 24,
            instructionText: {
                fontSize: 12,
                fontFamily: "Times New Roman",
                fontStyle: "italic"
            }
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

window.onerror = function (errorMessage, url, lineNumber) {
    alert(errorMessage + "\n" + url + "#" + lineNumber);
};

window.onload = () => {
    let canvas = document.getElementById("staff") as HTMLCanvasElement;

    //let fabricCanvas = new fabric.StaticCanvas(canvas, tablatureStyle.page);
    let fabricCanvas = new fabric.Canvas(canvas, tablatureStyle.page);

    renderer = new TR.PrimitiveRenderer(fabricCanvas, tablatureStyle);
    //renderer.drawFretNumber("2", 100, 100, true);
    //renderer.drawTitle("test!!!", 400, 100);
    //renderer.drawBarLine(Core.MusicTheory.BarLine.BeginAndEndRepeat, 100, 100);
    //renderer.drawFlag(BaseNoteValue.SixtyFourth, 100, 100, OffBarDirection.Top);
    //renderer.drawTuplet("3", 100, 100);
    //renderer.drawTie(100, 300, 100, OffBarDirection.Top);
};