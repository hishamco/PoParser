using System.Collections.Generic;

namespace PoParser.Core.Syntax
{
    public sealed class SyntaxTree
    {
        public SyntaxTree()
        {
            Statements = new List<Statement>();
        }

        public List<Statement> Statements { get; }
    }
}
