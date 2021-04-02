using Parlot;
using Parlot.Fluent;

namespace PoParser.Core
{
    public class PoParseContext : ParseContext
    {
        public PoParseContext(string text) : base(new Scanner(text))
        {
        }
    }
}
