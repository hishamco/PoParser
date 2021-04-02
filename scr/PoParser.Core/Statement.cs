using PoParser.Core.Syntax;
using System.Collections.Generic;

namespace PoParser.Core
{
    public abstract class Statement
    {
        public Statement()
        {
            Nodes = new List<SyntaxNode>();
        }

        public IList<SyntaxNode> Nodes { get; }
    }
}
