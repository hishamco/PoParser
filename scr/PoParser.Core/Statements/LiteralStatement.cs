using Parlot.Fluent;
using PoParser.Core.Syntax;
using static Parlot.Fluent.Parsers;

namespace PoParser.Core.Statements
{
    public class LiteralStatement : Statement
    {
        public static readonly Deferred<Statement> Statement = Deferred<Statement>();

        static LiteralStatement()
        {
            var doubleQuoteNode = new SyntaxNode(new SyntaxToken(SyntaxKind.DoubleQuoteToken, "\""));
            var literalNode = Terms.String(StringLiteralQuotes.Double).
                Then(c => new SyntaxNode(new SyntaxToken(SyntaxKind.StringToken, c.ToString())));

            Statement.Parser = literalNode
                .Then<Statement>(e =>
                {
                    var statement = new LiteralStatement(e.Token.Value.ToString());

                    statement.Nodes.Add(doubleQuoteNode);
                    statement.Nodes.Add(e);
                    statement.Nodes.Add(doubleQuoteNode);

                    return statement;
                });
        }

        public LiteralStatement(string text) : base(StatementKind.Literal)
        {
            Text = text;
        }

        public string Text { get; }
    }
}
