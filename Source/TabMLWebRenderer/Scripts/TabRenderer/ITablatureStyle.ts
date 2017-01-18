namespace TR {
    export interface ITablatureStyle {
        fallback: {
            fontFamily: string,
        }

        page: {
            width: number,
            height: number,
        },

        documentState: {
            transposition: ITablatureTextStyle,
            tempo: ITablatureTextStyle,
            section: ITablatureTextStyle,
            sectionTextPadding: number,
            alternativeEndingText: ITablatureTextStyle,
            alternativeEndingTextPadding: number,
            alternativeEndingHeight:number,
            endAlternativeEndingRightMargin: number
        },

        bar: {
            lineHeight: number;
            beamThickness: number,
            beamSpacing: number,
            timeSignature: ITablatureTextStyle,
            timeSignatureOffset: number
        }

        note: {
            margin: number,
            circleOnLongNotes: boolean,
            longNoteCirclePadding: number,
            dot: {
                radius: number,
                offset: number,
                spacing: number
            },
            flagSpacing : number,
            tuplet : ITablatureTextStyle,
            
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