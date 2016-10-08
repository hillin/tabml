using TabML.Parser.Parsing;

namespace TabML.Parser.Validation
{
    abstract class IntegrityChecker
    {
        public abstract bool CheckIntegrity(TablatureContext context, IReporter reporter);
    }
}
