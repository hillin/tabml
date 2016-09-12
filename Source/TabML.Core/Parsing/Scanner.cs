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
        public bool EndOfFile { get; private set; }

        private TextPointer _readRangeFrom;
        public TextRange LastReadRange { get; private set; }

        public Scanner(string input)
        {
            _lines = input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            this.Reset();
        }

        private IDisposable RecordReadRange()
        {
            return new ReadRangeScope(this);
        }


        public void Reset()
        {
            _textPointer.Row = 0;
            _textPointer.Column = 0;
            this.EndOfFile = false;
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
                this.EndOfFile = true;
            else if (_textPointer.Row == _lines.Length - 1 && _textPointer.Column >= _lines[_lines.Length - 1].Length)
                this.EndOfFile = true;

            this.EndOfFile = false;
        }

        private void MoveNext()
        {
            if (this.EndOfFile)
                return;

            ++_textPointer.Column;
            if (_textPointer.Column >= _lines[_textPointer.Row].Length)
                this.CarriageReturn();
            else
                this.CheckEndOfFile();
        }

        public char Peek()
        {
            return _lines[_textPointer.Row][_textPointer.Column];
        }

        public void Skip()
        {
            this.MoveNext();
        }

        public void Skip(Predicate<char> predicate)
        {
            while (!this.EndOfFile && predicate(this.Peek()))
                this.Skip();
        }

        public void SkipWhitespaces()
        {
            this.Skip(char.IsWhiteSpace);
        }

        public void SkipOptional(char optionalChar, bool skipWhitespaces = false)
        {
            if (skipWhitespaces)
                this.SkipWhitespaces();

            if (this.Peek() == optionalChar)
            {
                this.Skip();
                if (skipWhitespaces)
                    this.SkipWhitespaces();
            }
        }

        public void SkipLine()
        {
            this.CarriageReturn();
        }

        public void SkipUntil(char nextChar)
        {
            while (!this.EndOfFile && this.Peek() != nextChar)
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

        public char Read()
        {
            using (this.RecordReadRange())
            {
                var result = _lines[_textPointer.Row][_textPointer.Column];
                this.MoveNext();

                return result;
            }
        }

        public string Read(Predicate<char> predicate)
        {
            using (this.RecordReadRange())
            {
                var result = new StringBuilder();
                while (!this.EndOfFile)
                {
                    var chr = _lines[_textPointer.Row][_textPointer.Column];
                    if (predicate(chr))
                    {
                        result.Append(chr);
                        this.MoveNext();
                    }
                    else
                        break;
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
            bool includeNewline = false)
        {
            if (!this.Expect(open))
            {
                result = string.Empty;
                return ParenthesisReadResult.MissingOpen;
            }

            var builder = new StringBuilder();

            if (includeParenthesis)
                builder.Append(open);

            var nestLevel = 1;  // won't be increased if allowNesting is false

            while (!this.EndOfFile)
            {
                var chr = this.Peek();
                if ((chr == '\r' || chr == '\n') && !includeNewline)
                    break;

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
                            builder.Append(this.Read());
                        else
                            this.MoveNext();

                        break;
                    }
                }

                builder.Append(this.Read());
            }

            result = builder.ToString();
            return nestLevel == 0 ? ParenthesisReadResult.Success : ParenthesisReadResult.MissingClose;
        }
    }
}
