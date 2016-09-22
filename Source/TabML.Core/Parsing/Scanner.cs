using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace TabML.Core.Parsing
{
    class Scanner
    {
        public class Anchor
        {
            private readonly Scanner _owner;
            private readonly TextPointer _from;

            public TextRange Range => new TextRange(_from, _owner._textPointer);

            public Anchor(Scanner owner)
            {
                _owner = owner;
                _from = _owner._textPointer;
            }
        }

        private class ReadRangeScope : IDisposable
        {
            private readonly Scanner _owner;
            private readonly TextPointer _from;

            public ReadRangeScope(Scanner owner)
            {
                _owner = owner;
                _from = _owner._textPointer;
            }

            public void Dispose()
            {
                _owner.LastReadRange = new TextRange(_from, _owner._textPointer);
            }
        }

        private readonly string[] _lines;

        private TextPointer _textPointer;
        public TextPointer Pointer => _textPointer;
        public bool EndOfInput { get; private set; }
        public bool EndOfLine => _textPointer.Column >= _lines[_textPointer.Row].Length;
        public TextRange LastReadRange { get; private set; }

        public Scanner(string input)
        {
            _lines = input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            this.Reset();
        }

        public Anchor MakeAnchor()
        {
            return new Anchor(this);
        }

        private char GetCharacter(int row, int column)
        {
            if (column == _lines[row].Length && row != _lines.Length - 1)
                return '\n';

            return _lines[row][column];
        }

        private IDisposable RecordReadRange()
        {
            return new ReadRangeScope(this);
        }


        public void Reset()
        {
            _textPointer.Row = 0;
            _textPointer.Column = 0;
            this.EndOfInput = false;
        }

        private void CarriageReturn()
        {
            ++_textPointer.Row;
            _textPointer.Column = 0;
            this.CheckEndOfFile();
        }

        private void CheckEndOfFile()
        {
            if (_textPointer.Row >= _lines.Length)
                this.EndOfInput = true;
            else if (_textPointer.Row == _lines.Length - 1 && _textPointer.Column >= _lines[_lines.Length - 1].Length)
                this.EndOfInput = true;

            this.EndOfInput = false;
        }

        private void MoveNext(int offset = 1)
        {
            if (this.EndOfInput)
                return;

            _textPointer.Column += offset;

            if (this.EndOfLine)
                this.CarriageReturn();
            else
                this.CheckEndOfFile();
        }


        public void SetPointer(TextPointer pointer)
        {
            _textPointer = pointer;
            this.CheckEndOfFile();
        }

        public char Peek()
        {
            return this.GetCharacter(_textPointer.Row, _textPointer.Column);
        }

        public string Peek(int length, bool inline = true)
        {
            var savedPointer = _textPointer;

            var result = new StringBuilder();
            while (!this.EndOfInput && length > 0)
            {
                var chr = this.Peek();

                if (!Scanner.CheckInline(chr, inline))
                    break;

                result.Append(chr);
                this.MoveNext();

                --length;
            }

            this.SetPointer(savedPointer);

            return result.ToString();
        }


        public void Skip()
        {
            this.MoveNext();
        }

        public void Skip(Predicate<char> predicate)
        {
            while (!this.EndOfInput && predicate(this.Peek()))
                this.Skip();
        }

        public void SkipWhitespaces()
        {
            this.Skip(char.IsWhiteSpace);
        }

        public bool SkipOptional(char optionalChar, bool skipWhitespaces = false)
        {
            if (skipWhitespaces)
                this.SkipWhitespaces();

            if (this.Peek() == optionalChar)
            {
                this.Skip();
                if (skipWhitespaces)
                    this.SkipWhitespaces();

                return true;
            }

            return false;
        }

        public void SkipLine()
        {
            this.CarriageReturn();
        }

        public void SkipUntil(char nextChar)
        {
            while (!this.EndOfInput && this.Peek() != nextChar)
                this.Skip();
        }

        public bool Expect(char expectedChar)
        {
            using (this.RecordReadRange())
            {
                if (this.Peek() == expectedChar)
                {
                    this.Skip();
                    return true;
                }

                return false;
            }
        }


        public bool Expect(string value, bool ignoreCase = false)
        {
            using (this.RecordReadRange())
            {
                var remainingLine = this.GetRemainingLine();
                var stringComparison = ignoreCase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture;
                if (remainingLine.StartsWith(value, stringComparison))
                {
                    this.MoveNext(value.Length);
                    return true;
                }

                return false;
            }
        }

        public char Read()
        {
            using (this.RecordReadRange())
            {
                var result = _lines[_textPointer.Row][_textPointer.Column];
                this.MoveNext();

                return result;
            }
        }

        public string Read(Predicate<char> predicate, bool inline = true)
        {
            using (this.RecordReadRange())
            {
                var result = new StringBuilder();
                while (!this.EndOfInput)
                {
                    var chr = _lines[_textPointer.Row][_textPointer.Column];

                    if (!Scanner.CheckInline(chr, inline))
                    {
                        this.MoveNext();    // move to the next line if new line encountered
                        break;
                    }

                    if (!predicate(chr))
                        break;

                    result.Append(chr);
                    this.MoveNext();
                }

                return result.ToString();
            }
        }

        public string Read(string pattern)
        {
            using (this.RecordReadRange())
            {
                var remainingLine = this.GetRemainingLine();
                var match = Regex.Match(remainingLine, $"^{pattern}");

                if (!match.Success)
                    return string.Empty;

                var result = match.Value;
                _textPointer.Column += result.Length - 1;
                this.MoveNext();
                return result;
            }
        }

        public string ReadToLineEnd()
        {
            using (this.RecordReadRange())
            {
                var result = this.GetRemainingLine();
                this.CarriageReturn();
                return result;
            }
        }

        private string GetRemainingLine()
        {
            return _lines[_textPointer.Row].Substring(_textPointer.Column);
        }

        public Match Match(string pattern)
        {
            using (this.RecordReadRange())
            {
                var remainingLine = this.GetRemainingLine();
                var match = Regex.Match(remainingLine, $"^{pattern}");

                if (match.Success)
                {
                    _textPointer.Column += match.Value.Length - 1;
                    this.MoveNext();
                }

                return match;
            }
        }

        public bool MatchSingle(string pattern, out string result)
        {
            var match = this.Match(pattern);
            if (!match.Success || match.Groups.Count < 2)
            {
                result = string.Empty;
                return false;
            }

            result = match.Groups[1].Value;
            return true;
        }

        public bool TryReadInteger(out int value)
        {
            var text = this.Read(char.IsDigit);
            return int.TryParse(text, out value);
        }

        public enum ParenthesisReadResult
        {
            Success,
            MissingOpen,
            MissingClose,
        }

        public ParenthesisReadResult TryReadParenthesis(out string result,
            char open = '(',
            char close = ')',
            bool includeParenthesis = false,
            bool allowNesting = true,
            bool inline = true)
        {
            Anchor anchor = null;

            if (includeParenthesis)
                anchor = this.MakeAnchor();

            if (!this.Expect(open))
            {
                result = string.Empty;
                return ParenthesisReadResult.MissingOpen;
            }

            if (!includeParenthesis)
                anchor = this.MakeAnchor();

            var builder = new StringBuilder();

            if (includeParenthesis)
                builder.Append(open);

            var nestLevel = 1;  // won't be increased if allowNesting is false

            while (!this.EndOfInput)
            {
                var chr = this.Peek();

                if (!Scanner.CheckInline(chr, inline))
                {
                    this.MoveNext();
                    result = builder.ToString();
                    return ParenthesisReadResult.MissingClose;
                }

                if (chr == open && allowNesting)
                {
                    ++nestLevel;
                }
                else if (chr == close)
                {
                    --nestLevel;
                    if (nestLevel == 0)
                    {
                        if (includeParenthesis)
                        {
                            builder.Append(this.Read());
                            this.LastReadRange = anchor.Range;
                        }
                        else
                        {
                            this.LastReadRange = anchor.Range;
                            this.MoveNext();
                        }

                        break;
                    }
                }

                builder.Append(this.Read());
            }

            result = builder.ToString();
            return nestLevel == 0 ? ParenthesisReadResult.Success : ParenthesisReadResult.MissingClose;
        }

        private static bool CheckInline(char c, bool inline)
        {
            return !(inline && c == '\n');
        }
    }
}
