using TabML.Core.Document;
using TabML.Core.Logging;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    interface IDocumentElementFactory<T>
        where T : Element
    {
        bool ToDocumentElement(TablatureContext context, ILogger logger, out T element);
    }
}
