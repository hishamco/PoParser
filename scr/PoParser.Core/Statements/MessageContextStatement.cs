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
                Then(c => new SyntaxNode(new SyntaxToken(SyntaxKind.MessageContextToken, c)));
            var contextNode = Terms.String(StringLiteralQuotes.Double).
                Then(c => new SyntaxNode(new SyntaxToken(SyntaxKind.StringToken, c.ToString())));
            var messageContextStatement = messageContextNode.And(contextNode);

            Statement.Parser = messageContextStatement
                .Then<Statement>(e =>
                {
                    var statement = new MessageContextStatement(e.Item2.Token.Value.ToString());

                    statement.Nodes.Add(e.Item1);
                    statement.Nodes.Add(e.Item2);

                    return statement;
                });
        }

        public MessageContextStatement(string context)
        {

            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public string Context { get; }
    }
}
