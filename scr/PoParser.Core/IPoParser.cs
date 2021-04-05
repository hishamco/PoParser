using System.Collections.Generic;

namespace PoParser.Core
{
    public interface IPoParser
    {
        IEnumerable<Statement> Parse(string content);
    }
}
