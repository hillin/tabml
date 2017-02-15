namespace Core.MusicTheory {
    export type IExplicitChordFingeringNote = { fret: number, finger?: number, important?:boolean }
    export type IChordFingeringNote = "x" | number | IExplicitChordFingeringNote
    export type IChordFingering = IChordFingeringNote[]
}