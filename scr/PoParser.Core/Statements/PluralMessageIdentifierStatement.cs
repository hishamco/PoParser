using Parlot.Fluent;
using PoParser.Core.Syntax;
using System;
using static Parlot.Fluent.Parsers;

namespace PoParser.Core.Statements
{
    public class PluralMessageIdentifierStatement : Statement
    {
        public static readonly Deferred<Statement> Statement = Deferred<Statement>();

        static PluralMessageIdentifierStatement()
        {
            var messageIdNode = Terms.Text("msgid_plural ").
                Then(c => new SyntaxNode(new SyntaxToken(SyntaxKind.MessageIdentifierToken, c)));
            var identifierNode = Terms.String(StringLiteralQuotes.Double).
                Then(c => new SyntaxNode(new SyntaxToken(SyntaxKind.StringToken, c.ToString())));
            var messageIdentifierStatement = messageIdNode.And(identifierNode);

            Statement.Parser = messageIdentifierStatement
                .Then<Statement>(e =>
                {
                    var statement = new PluralMessageIdentifierStatement(e.Item2.Token.Value.ToString());

                    statement.Nodes.Add(e.Item1);
                    statement.Nodes.Add(e.Item2);

                    return statement;
                });
        }

        public PluralMessageIdentifierStatement(string identifier)
        {

            Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
        }

        public string Identifier { get; }
    }
}
