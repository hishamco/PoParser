using Parlot.Fluent;
using PoParser.Core.Syntax;
using System;
using static Parlot.Fluent.Parsers;

namespace PoParser.Core.Statements
{
    public class MessageContextStatement : Statement
    {
        public static readonly Deferred<Statement> Statement = Deferred<Statement>();

        static MessageContextStatement()
        {
            var messageContextNode = Terms.Text("msgctxt ").
                Then(c => new SyntaxNode(new SyntaxToken(SyntaxKind.IdentifierToken, c.TrimEnd())));
            var doubleQuoteNode = new SyntaxNode(new SyntaxToken(SyntaxKind.DoubleQuoteToken, "\""));
            var contextNode = Terms.String(StringLiteralQuotes.Double).
                Then(c => new SyntaxNode(new SyntaxToken(SyntaxKind.StringToken, c.ToString())));

            Statement.Parser = messageContextNode.And(contextNode)
                .Then<Statement>(e =>
                {
                    var context = e.Item2.Token.Value.ToString();
                    var statement = new MessageContextStatement(context);

                    statement.Nodes.Add(e.Item1);
                    statement.Nodes.Add(doubleQuoteNode);
                    statement.Nodes.Add(new SyntaxNode(new SyntaxToken(SyntaxKind.StringToken, context)));
                    statement.Nodes.Add(doubleQuoteNode);

                    return statement;
                });
        }

        public MessageContextStatement(string context) : base(StatementKind.Context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public string Context { get; }
    }
}
