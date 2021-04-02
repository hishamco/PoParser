using Parlot.Fluent;
using PoParser.Core.Syntax;
using static Parlot.Fluent.Parsers;

namespace PoParser.Core.Statements
{
    public class CommentStatement : Statement
    {
        private static readonly Parser<char> _token = Terms.Char('#');
        public static readonly Deferred<Statement> Statement = Deferred<Statement>();

        static CommentStatement()
        {
            Statement.Parser = _token.SkipAnd(Terms.Pattern(p => true))
                .Then<Statement>(e =>
                {
                    var statement = new CommentStatement();
                    var node = new SyntaxNode(new SyntaxToken(SyntaxKind.CommentTrivia, e.ToString()));

                    statement.Nodes.Add(node);

                    return statement;
                });
        }
    }
}
