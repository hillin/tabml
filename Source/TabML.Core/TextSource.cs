using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core
{
    public class TextSource
    {
        private readonly string[] _lines;

        public TextSource(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            this._lines = input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            for (var i = 0; i < _lines.Length - 1; ++i)
                _lines[i] = string.Concat(_lines[i], "\n");
        }

        public string Substring(TextRange textRange)
        {
            var builder = new StringBuilder();

            for (var p = textRange.From; p < textRange.To;)
            {
                var row = _lines[p.Row];
                builder.Append(row[p.Column]);
                ++p.Column;
                if (p.Column >= row.Length)
                {
                    ++p.Row;
                    p.Column = 0;
                }
            }

            return builder.ToString();
        }

        public bool IsEndOfSource(TextPointer pointer)
        {
            if (pointer.Row >= _lines.Length)
                return true;

            if (pointer.Row == _lines.Length - 1
                && pointer.Column >= _lines[pointer.Row].Length)
                return true;

            return false;
        }

        public char this[int row, int column] => _lines[row][column];
        public string this[int row] => _lines[row];
    }
}
