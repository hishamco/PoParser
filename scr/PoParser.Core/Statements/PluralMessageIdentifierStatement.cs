﻿using Parlot.Fluent;
using PoParser.Core.Syntax;
using System;
using System.Linq;
using static Parlot.Fluent.Parsers;

namespace PoParser.Core.Statements
{
    public class PluralMessageIdentifierStatement : Statement
    {
        public static readonly Deferred<Statement> Statement = Deferred<Statement>();

        static PluralMessageIdentifierStatement()
        {
            var messageIdNode = Terms.Text("msgid_plural ").
                Then(c => new SyntaxNode(new SyntaxToken(SyntaxKind.IdentifierToken, c.TrimEnd())));
            var doubleQuoteNode = new SyntaxNode(new SyntaxToken(SyntaxKind.DoubleQuoteToken, "\""));
            var identifierNode = Terms.String(StringLiteralQuotes.Double).
                Then(c => new SyntaxNode(new SyntaxToken(SyntaxKind.StringToken, c.ToString())));

            Statement.Parser = messageIdNode.And(ZeroOrMany(identifierNode))
                .Then<Statement>(e =>
                {
                    var identifier = e.Item2.Count == 1
                        ? e.Item2[0].Token.Value.ToString()
                        : String.Join(String.Empty, e.Item2.Select(n => n.Token.Value.ToString()));
                    var statement = new PluralMessageIdentifierStatement(identifier);

                    statement.Nodes.Add(e.Item1);

                    statement.Nodes.Add(doubleQuoteNode);
                    statement.Nodes.Add(new SyntaxNode(new SyntaxToken(SyntaxKind.StringToken, identifier)));
                    statement.Nodes.Add(doubleQuoteNode);

                    return statement;
                });
        }

        public PluralMessageIdentifierStatement(string identifier) : base(StatementKind.PluralMessageId)
        {

            Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
        }

        public string Identifier { get; }
    }
}
