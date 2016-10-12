using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;
using TabML.Parser.Parsing;
using static TabML.Core.MusicTheory.Intervals;

namespace TabML.Parser.AST
{
    class ChordParser
    {
        private enum TriadQuality
        {
            Diminished,
            Minor,
            Major,
            Augmented
        }

        private Scanner _scanner;
        private List<Interval> _intervals;
        private TriadQuality _triadQuality;
        private bool _addedToneRead;

        private bool _hasError;

        private void AddIntervals(params Interval[] intervals)
        {
            _intervals.AddRange(intervals);
        }

        public bool TryParse(string chordName, out Chord chord)
        {
            chord = null;

            _scanner = new Scanner(chordName.Trim());
            NoteNameNode noteNameNode;
            if (!new NoteNameParser().TryParse(_scanner, out noteNameNode))
                return false;

            var root = noteNameNode.ToNoteName();

            if (_scanner.EndOfInput)
            {
                chord = Chord.Construct(chordName, root, M3, P5);
                return true;
            }

            _intervals = new List<Interval>();

            var isFifth = false;

            if (_scanner.Expect("5")) // 5th
            {
                this.AddIntervals(P5);
                isFifth = true;
            }
            else if (_scanner.Expect("ø7")) // half diminished seventh
            {
                this.AddIntervals(m3, d5, m7);
            }
            else
            {
                if (!this.ReadDominant())
                {
                    if (_hasError)
                        return false;

                    if (this.ReadTriad())
                    {
                        if (!this.ReadSeventh())
                        {
                            this.ReadExtended();
                        }
                    }
                    else
                    {
                        if (_hasError)
                            return false;

                        this.ReadSimplifiedAddedTone();
                    }
                }
            }

            if (_hasError)
                return false;

            NoteName? bass = null;
            if (!isFifth)
            {
                this.ReadSuspended();
                if (_hasError)
                    return false;

                if (!_addedToneRead)
                {
                    this.ReadAddedTone();
                    if (_hasError)
                        return false;
                }

                this.ReadAltered();
                if (_hasError)
                    return false;

                this.ReadBass(out bass);
                if (_hasError)
                    return false;
            }

            _scanner.SkipWhitespaces();
            if (!_scanner.EndOfInput)
            {
                _hasError = true;   //unexpected
                return false;
            }

            chord = Chord.Construct(chordName, root, _intervals.ToArray());
            chord.Bass = bass;
            
            return true;
        }

        private bool ReadExtended()
        {
            switch (_scanner.ReadAny("9", "11", "13"))
            {
                case "9":
                    switch (_triadQuality)
                    {
                        case TriadQuality.Diminished:
                            _hasError = true;   // Cdim9 not supported
                            return false;
                        case TriadQuality.Minor:
                            this.AddIntervals(m7, M9);  // Cm9
                            return true;
                        case TriadQuality.Major:
                            this.AddIntervals(M7, M9);  // CM9
                            return true;
                        case TriadQuality.Augmented:
                            this.AddIntervals(m7, M9);  // Caug9
                            return true;
                    }
                    _hasError = true;
                    return false;
                case "11":
                    switch (_triadQuality)
                    {
                        case TriadQuality.Diminished:
                            _hasError = true;   // Cdim11 not supported
                            return false;
                        case TriadQuality.Minor:
                            this.AddIntervals(m7, M9, P11);  // Cm11
                            return true;
                        case TriadQuality.Major:
                            this.AddIntervals(M7, M9, P11);  // CM11
                            return true;
                        case TriadQuality.Augmented:
                            this.AddIntervals(m7, M9, P11);  // Caug11
                            return true;
                    }
                    _hasError = true;
                    return false;
                case "13":
                    switch (_triadQuality)
                    {
                        case TriadQuality.Diminished:
                            _hasError = true;   // Cdim13 not supported
                            return false;
                        case TriadQuality.Minor:
                            this.AddIntervals(m7, M9, P11, M13);  // Cm13
                            return true;
                        case TriadQuality.Major:
                            this.AddIntervals(M7, M9, P11, M13);  // CM13
                            return true;
                        case TriadQuality.Augmented:
                            this.AddIntervals(m7, M9, P11, M13);  // Caug13
                            return true;
                    }
                    _hasError = true;
                    return false;
            }

            return false;
        }

        private bool ReadBass(out NoteName? bass)
        {
            bass = null;
            if (!_scanner.Expect('/'))
                return false;

            NoteNameNode noteNameNode;
            if (!new NoteNameParser().TryParse(_scanner, out noteNameNode))
            {
                _hasError = true;   // missing bass note
                return false;
            }

            bass = noteNameNode.ToNoteName();
            return true;
        }

        private bool ReadAltered()
        {
            _scanner.SkipWhitespaces();
            switch (_scanner.ReadAny(@"\+5", @"\#5", "♯5",
                                     @"\-9", "b9", "♭9",
                                     @"\+9", @"\#9", "♯9",
                                     @"\+11", @"\#11", "♯11"))
            {
                case "+5":
                case "#5":
                case "♯5":
                    if (_intervals.Count < 3)
                    {
                        _hasError = true;   // only available to 7th+
                        return false;
                    }

                    if (_intervals[1] == A5)
                    {
                        // already has it
                    }

                    _intervals[1] = A5; return true;
                case "-9":
                case "b9":
                case "♭9":
                    if (_intervals.Count < 5)
                    {
                        _hasError = true;   // only available to 11th+
                        return false;
                    }

                    if (_intervals[3] == m9)
                    {
                        // already has it
                    }

                    _intervals[3] = m9; return true;
                case "+9":
                case "#9":
                case "♯9":
                    if (_intervals.Count < 5)
                    {
                        _hasError = true;   // only available to 11th+
                        return false;
                    }

                    if (_intervals[3] == A9)
                    {
                        // already has it
                    }

                    _intervals[3] = A9; return true;
                case "+11":
                case "#11":
                case "♯11":
                    if (_intervals.Count < 6)
                    {
                        _hasError = true;   // only available to 13th+
                        return false;
                    }

                    if (_intervals[4] == A11)
                    {
                        // already has it
                    }

                    _intervals[4] = A11; return true;
            }

            return false;
        }

        private bool ReadSuspended()
        {
            _scanner.SkipWhitespaces();
            switch (_scanner.ReadAny("sus2", "sus4", "sus"))
            {
                case "sus2":

                    if (_intervals[0] != M3)
                    {
                        _hasError = true;
                        return false;
                    }

                    _intervals[0] = M2;
                    return true;
                case "sus4":
                case "sus":
                    if (_intervals[0] != M3)
                    {
                        _hasError = true;
                        return false;
                    }

                    _intervals[0] = P4;
                    return true;
            }

            return false;
        }

        private bool ReadAddedTone()
        {
            _scanner.SkipWhitespaces();
            switch (_scanner.ReadAny(@"add\#9", "add♯9", "addb9", "add♭9", "add9", @"add\#11", "add♯11", "add11", @"add\#13", "add♯13", "addb13", "add♭13", "add13"))
            {
                case "add#9":
                case "add♯9":

                    if (_intervals.Count > 2)
                    {
                        _hasError = true;   // only available to triads
                        return false;
                    }

                    this.AddIntervals(A9);
                    return true;
                case "addb9":
                case "add♭9":

                    if (_intervals.Count > 2)
                    {
                        _hasError = true;   // only available to triads
                        return false;
                    }

                    this.AddIntervals(m9);
                    return true;
                case "add9":

                    if (_intervals.Count > 2)
                    {
                        _hasError = true;   // only available to triads
                        return false;
                    }

                    this.AddIntervals(M9);
                    return true;
                case "add#11":
                case "add♯11":

                    if (_intervals.Count > 3)
                    {
                        _hasError = true;   // only available to triads or seventh
                        return false;
                    }

                    this.AddIntervals(A11);
                    return true;
                case "add11":

                    if (_intervals.Count > 3)
                    {
                        _hasError = true;   // only available to triads or seventh
                        return false;
                    }

                    this.AddIntervals(P11);
                    return true;
                case "add#13":
                case "add♯13":

                    if (_intervals.Count > 4)
                    {
                        _hasError = true;   // only available to triads, sevenths, or ninths
                        return false;
                    }

                    this.AddIntervals(A13);
                    return true;
                case "addb13":
                case "add♭13":

                    if (_intervals.Count > 4)
                    {
                        _hasError = true;   // only available to triads, sevenths, or ninths
                        return false;
                    }

                    this.AddIntervals(m13);
                    return true;
                case "add13":

                    if (_intervals.Count > 4)
                    {
                        _hasError = true;   // only available to triads, sevenths, or ninths
                        return false;
                    }

                    this.AddIntervals(M13);
                    return true;
            }

            return false;
        }

        private bool ReadSimplifiedAddedTone()
        {
            switch (_scanner.ReadAny(@"6\/9", "69", "2", "4", "6"))
            {
                case "6/9":
                case "69":
                    this.AddIntervals(M6, M9);
                    _addedToneRead = true;
                    return true;
                case "2":
                    this.AddIntervals(M9);
                    _addedToneRead = true;
                    return true;
                case "4":
                    this.AddIntervals(P11);
                    _addedToneRead = true;
                    return true;
                case "6":
                    this.AddIntervals(M13);
                    _addedToneRead = true;
                    return true;
            }
            return false;
        }

        private bool ReadDominant()
        {
            switch (_scanner.ReadAny("dom7", "7", "9", "11", "13"))
            {
                case "dom7":
                case "7":
                    this.AddIntervals(M3, P5, m7);
                    return true;
                case "9":
                    this.AddIntervals(M3, P5, m7, M9);
                    return true;
                case "11":
                    this.AddIntervals(M3, P5, m7, M9, P11);
                    return true;
                case "13":
                    this.AddIntervals(M3, P5, m7, M9, P11, M13);
                    return true;
            }

            return false;
        }

        private bool ReadSeventh()
        {
            _scanner.SkipWhitespaces();
            switch (_scanner.ReadAny("maj7", "M7", "Δ7", "7"))
            {
                case "maj7":
                case "M7":
                case "Δ7":
                    this.AddIntervals(M7);  // CmM7
                    return true;
                case "7":
                    switch (_triadQuality)
                    {
                        case TriadQuality.Diminished:
                            this.AddIntervals(d7);  // Cdim7
                            return true;
                        case TriadQuality.Minor:
                            this.AddIntervals(m7);  // Cm7
                            return true;
                        case TriadQuality.Major:
                            this.AddIntervals(M7);  // CM7
                            return true;
                        case TriadQuality.Augmented:
                            this.AddIntervals(m7);  // Caug7
                            return true;
                    }
                    break;
            }

            return false;
        }

        private bool ReadTriad()
        {
            switch (_scanner.ReadAny("maj", "min", "aug", "dim", "M", "m", "Δ", @"\+", @"\-", "°"))
            {
                case "":
                case "maj":
                case "M":
                case "Δ":
                    this.AddIntervals(M3, P5);
                    _triadQuality = TriadQuality.Major;
                    return true;
                case "min":
                case "m":
                    this.AddIntervals(m3, P5);
                    _triadQuality = TriadQuality.Minor;
                    return true;
                case "aug":
                case "+":
                    this.AddIntervals(M3, A5);
                    _triadQuality = TriadQuality.Augmented;
                    return true;
                case "dim":
                case "-":
                case "°":
                    this.AddIntervals(m3, d5);
                    _triadQuality = TriadQuality.Diminished;
                    return true;
            }

            return false;
        }
    }
}
