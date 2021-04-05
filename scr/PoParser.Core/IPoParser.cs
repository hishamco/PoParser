using PoParser.Core.Syntax;

namespace PoParser.Core
{
    public interface IPoParser
    {
        SyntaxTree Parse(string content);
    }
}
