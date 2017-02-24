namespace TR {
    export interface ITablatureStyle {

        stringCount: number;

        smuflText: {
            fontFamily: string,
            fontSize: number,
        }

        fallback: {
            fontFamily: string,
        }

        page: {
            width: number,
            height: number,
        }

        documentState: {
            transposition: ITablatureTextStyle,
            tempo: ITablatureTextStyle,
            section: ITablatureTextStyle,
            sectionTextPadding: number,
            alternativeEndingText: ITablatureTextStyle,
            alternativeEndingTextPadding: number,
            alternativeEndingHeight:number,
            endAlternativeEndingRightMargin: number
        }

        bar: {
            lineHeight: number;
            beamThickness: number,
            beamSpacing: number,
            timeSignature: ITablatureTextStyle,
            timeSignatureOffset: number
        }

        note: {
            margin: number,
            ellipseAroundLongNotes: boolean,
            longNoteEllipsePadding: number,
            dot: {
                radius: number,
                offset: number,
                spacing: number
            },
            flagSpacing : number,
            tuplet : ITablatureTextStyle,
            
        }

        chordDiagram : {
            gridThickness: number,
            nutThickness: number,
            elementSpacing: number,
            cellWidth: number,
            cellHeight: number,
            nameText: ITablatureTextStyle,
            fingeringText: ITablatureTextStyle,
            fretText: ITablatureTextStyle,
            fingeringTokenRadius: number,
            specialStringTokenPadding: { top:number, bottom:number }
        }

        ornaments : {
            connectionInstructionText: ITablatureTextStyle,
            artificialHarmonicsText: ITablatureTextStyle,
            rasgueadoText: ITablatureTextStyle
        }

        title: ITablatureTextStyle;
        fretNumber: ITablatureTextStyle;
        lyrics: ITablatureTextStyle;
    }
}