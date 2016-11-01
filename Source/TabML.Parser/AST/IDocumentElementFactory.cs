using TabML.Core.Document;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    interface IDocumentElementFactory<T>
        where T : Element
    {
        bool ToDocumentElement(TablatureContext context, IReporter reporter, out T element);
    }
}
