using PoParser.Core.Syntax;
using System.Collections.Generic;

namespace PoParser.Core
{
    public abstract class Statement
    {
        public Statement(StatementKind kind)
        {
            Kind = kind;
            Nodes = new List<SyntaxNode>();
        }

        public StatementKind Kind { get; }

        public IList<SyntaxNode> Nodes { get; }
    }
}
