
interface ITablatureStyle
{
    fallback: {
        fontFamily: string,
    } 

    page : { 
        width: number,
        height: number,
    }

    title: ITablatureTextStyle;
    fretNumber: ITablatureTextStyle;
    lyrics: ITablatureTextStyle;
}