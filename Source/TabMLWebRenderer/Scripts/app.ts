
let tablatureStyle: TR.ITablatureStyle =
    {
        stringCount: 6,

        smuflText: {
            fontFamily: "Bravura"
        },

        fallback: {
            fontFamily: "Segoe UI",
        },

        page: {
            width: 1200,
            height: 3200
        },

        documentState: {
            transposition: {
                fontSize: 12,
                fontFamily: "Times New Roman",
                fontStyle: "bold"
            },
            tempo: {
                fontSize: 12,
                fontFamily: "Bravura",
                fontStyle: "bold"
            },
            section: {
                fontSize: 14,
                fontFamily: "Times New Roman",
                fontStyle: "bold"
            },
            sectionTextPadding: 2,
            alternativeEndingText: {
                fontSize: 12,
                fontFamily: "Times New Roman",
                fontStyle: "bold"
            },
            alternativeEndingHeight: 16,
            alternativeEndingTextPadding: 2,
            endAlternativeEndingRightMargin: 4
        },

        bar: {
            lineHeight: 12,
            beamThickness: 4,
            beamSpacing: 4,
            timeSignature: {
                fontSize: 42,
                fontFamily: "Bravura",
                lineHeight: 0.5
            },
            timeSignatureOffset: -12,
        },

        note: {
            margin: 2,
            circleOnLongNotes: true,
            longNoteCirclePadding: 1,
            dot: {
                radius: 1.5,
                offset: 3,
                spacing: 2
            },
            flagSpacing: 4,
            tuplet: {
                fontSize: 20,
                fontFamily: "Bravura",
            },

        },

        chordDiagram : {
            gridThickness: 1,
            nutThickness: 3,
            elementSpacing: 2,
            cellHeight: 11,
            cellWidth: 7,
            nameText: {
                fontSize: 14,
                fontFamily: "Times New Roman",
                fontStyle: "bold"
            },
            fingeringText: {
                fontSize: 10,
                fontFamily: "Times New Roman",
                fontStyle: "italic"
            },
            fretText: {
                fontSize: 10,
                fontFamily: "Times New Roman",
                fontStyle: "italic"
            },
            fingeringTokenRadius: 2.5,
            specialStringTokenPadding: { top:-24, bottom:-8 }
        },

        ornaments: {
            artificialHarmonicsText : {
                fontSize: 12,
                fontFamily: "Times New Roman",
                fontStyle: "bold"
            },
            connectionInstructionText: {
                fontSize: 12,
                fontFamily: "Times New Roman",
                fontStyle: "italic"
            },
            rasgueadoText: {
                fontSize: 12,
                fontFamily: "Times New Roman",
                fontStyle: "bold italic"
            },
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

let callbackObjects: Array<any>;

let renderer: TR.PrimitiveRenderer;

window.onerror = function (errorMessage, url, lineNumber) {
    alert(errorMessage + "\n" + url + "#" + lineNumber);
};

window.onload = () => {
    let canvas = document.getElementById("staff") as HTMLCanvasElement;

    //let fabricCanvas = new fabric.StaticCanvas(canvas, tablatureStyle.page);
    let fabricCanvas = new fabric.Canvas(canvas, tablatureStyle.page);

    renderer = new TR.PrimitiveRenderer(fabricCanvas, tablatureStyle);
    renderer.drawChord(113.557998657227, 169.242, "F#m", [{fret:2,finger:1,},{fret:4,finger:3,},{fret:4,finger:4,},{fret:2,finger:1,},{fret:2,finger:1,},{fret:2,finger:1,},]);

    renderer.drawChord(440, 169.242, "D", ['x','x',0,{fret:2,finger:1,},{fret:3,finger:3,},{fret:2,finger:2,},])
    //renderer.drawFretNumber("2", 100, 100, true);
    //renderer.drawTitle("test!!!", 400, 100);
    //renderer.drawBarLine(Core.MusicTheory.BarLine.BeginAndEndRepeat, 100, 100);
    //renderer.drawFlag(BaseNoteValue.SixtyFourth, 100, 100, OffBarDirection.Top);
    //renderer.drawTuplet("3", 100, 100);
    //renderer.drawTie(100, 300, 100, OffBarDirection.Top);
};