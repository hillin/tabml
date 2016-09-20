namespace TabML.Core.Parsing.AST
{
    public abstract class Node
    {
        private TextRange _range;

        public TextRange Range
        {
            get { return _range; }
            set { _range = value; }
        }

        public void SetRangeFrom(TextPointer from)
        {
            _range.From = from;
        }

        public void SetRangeTo(TextPointer to)
        {
            _range.To = to;
        }
    }
}
