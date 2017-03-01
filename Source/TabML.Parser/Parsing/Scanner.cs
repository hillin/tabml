using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using TabML.Core;
using TabML.Core.Parsing;

namespace TabML.Parser.Parsing
{
    [DebuggerDisplay("{Pointer}: ...{RemainingLine}...")]
    class Scanner
    {
        public class Anchor
        {
            private readonly Scanner _owner;
            private readonly TextPointer _from;

            public bool TrimTailingWhitespaces { get; set; }

            public TextRange Range
            {
                get
                {
                    var to = _owner._textPointer;

                    if (!this.TrimTailingWhitespaces)
                        return new TextRange(_from, to, _owner.Source);

                    var row = _owner.Source[to.Row];

                    while (to > _from)
                    {
                        if (to.Column == 0)
                        {
                            --to.Row;
                            to.Column = _owner.Source[to.Row].Length - 1; // should be '\n'
                            row = _owner.Source[to.Row];
                            continue;
                        }

                        if (!char.IsWhiteSpace(row[to.Column - 1]))
                            break;

                        --to.Column;
                    }

                    return new TextRange(_from, to, _owner.Source);
                }
            }

            public Anchor(Scanner owner, bool trimTailingWhitespaces)
            {
                _owner = owner;
                _from = _owner._textPointer;
                this.TrimTailingWhitespaces = trimTailingWhitespaces;
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
                _owner.LastReadRange = new TextRange(_from, _owner._textPointer, _owner.Source);
            }
        }

        public TextSource Source { get; }
        private TextPointer _textPointer;
        public TextPointer Pointer => _textPointer;
        public bool EndOfInput { get; private set; }
        public bool EndOfLine => this.EndOfInput || _textPointer.Column >= this.Source[_textPointer.Row].Length || this.Peek() == '\n';
        public TextRange LastReadRange { get; private set; }

        public string RemainingLine
        {
            get
            {
                var row = this.Source[_textPointer.Row];
                if (_textPointer.Column >= row.Length)
                    return string.Empty;

                var result = row.Substring(_textPointer.Column);
                return result[result.Length - 1] == '\n' ? result.Substring(0, result.Length - 1) : result;
            }
        }

        public Scanner(TextSource source)
        {
            this.Source = source;
            this.Reset();
        }

        public Scanner(string source)
            : this(new TextSource(source))
        {

        }

        public Anchor MakeAnchor(bool trimTailingWhitespaces = true)
        {
            return new Anchor(this, trimTailingWhitespaces);
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
            this.EndOfInput = this.Source.IsEndOfSource(_textPointer);
        }

        private void MoveNext(int offset = 1)
        {
            if (this.EndOfInput)
                return;

            if (this.EndOfLine)
            {
                this.CarriageReturn();
            }
            else
            {
                _textPointer.Column += offset;
                this.CheckEndOfFile();
            }
        }


        public void SetPointer(TextPointer pointer)
        {
            _textPointer = pointer;
            this.CheckEndOfFile();
        }

        public char Peek()
        {
            return this.Source[_textPointer.Row][_textPointer.Column];
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

        public void SkipWhitespaces(bool inline = true)
        {
            if (inline)
                this.Skip(c => char.IsWhiteSpace(c) && c != '\n');
            else
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
            if (this.EndOfInput)
                return false;

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
            if (this.EndOfInput)
                return false;

            using (this.RecordReadRange())
            {
                var remainingLine = this.RemainingLine;
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
                var result = this.Source[_textPointer.Row][_textPointer.Column];
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
                    var chr = this.Peek();

                    if (!Scanner.CheckInline(chr, inline))
                    {
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

        private string ReadPattern(string pattern)
        {
            using (this.RecordReadRange())
            {
                var remainingLine = this.RemainingLine;
                var match = Regex.Match(remainingLine, pattern);

                if (!match.Success)
                    return string.Empty;

                var result = match.Value;
                _textPointer.Column += result.Length - 1;
                this.MoveNext();
                return result;
            }
        }

        /// <remarks>
        /// if an OR ('|') syntax is used, you must enclose the pattern
        /// with parenthesises
        /// </remarks>
        public string Read(string pattern)
        {
            return this.ReadPattern($"^{pattern}");
        }

        public string ReadAny(params string[] patterns)
        {
            if (patterns == null || patterns.Length == 0)
                return string.Empty;

            if (patterns.Length == 1)
                return this.Read(patterns[0]);

            var builder = new StringBuilder();
            builder.Append('^');
            builder.Append('(');
            builder.Append(string.Join("|", patterns));
            builder.Append(')');

            return this.ReadPattern(builder.ToString());
        }

        public string ReadToLineEnd()
        {
            using (this.RecordReadRange())
            {
                var result = this.RemainingLine;
                this.SetPointer(_textPointer.OffsetColumn(result.Length));
                return result;
            }
        }


        public Match Match(string pattern)
        {
            using (this.RecordReadRange())
            {
                var remainingLine = this.RemainingLine;
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
