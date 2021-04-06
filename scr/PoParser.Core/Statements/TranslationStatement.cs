using Parlot.Fluent;
using PoParser.Core.Syntax;
using System;
using System.Linq;
using static Parlot.Fluent.Parsers;

namespace PoParser.Core.Statements
{
    public class TranslationStatement : Statement
    {
        public static readonly Deferred<Statement> Statement = Deferred<Statement>();

        static TranslationStatement()
        {
            var messageTranslationNode = Terms.Text("msgstr ").
                Then(c => new SyntaxNode(new SyntaxToken(SyntaxKind.IdentifierToken, c.TrimEnd())));
            var doubleQuoteNode = new SyntaxNode(new SyntaxToken(SyntaxKind.DoubleQuoteToken, "\""));
            var translationNode = Terms.String(StringLiteralQuotes.Double).
                Then(c => new SyntaxNode(new SyntaxToken(SyntaxKind.StringToken, c.ToString())));

            Statement.Parser = messageTranslationNode.And(ZeroOrMany(translationNode))
                .Then<Statement>(e =>
                {
                    var identifier = e.Item2.Count == 1
                        ? e.Item2[0].Token.Value.ToString()
                        : String.Join(String.Empty, e.Item2.Select(n => n.Token.Value.ToString()));
                    var statement = new TranslationStatement(identifier);

                    statement.Nodes.Add(e.Item1);
                    statement.Nodes.Add(doubleQuoteNode);
                    statement.Nodes.Add(new SyntaxNode(new SyntaxToken(SyntaxKind.StringToken, identifier)));
                    statement.Nodes.Add(doubleQuoteNode);

                    return statement;
                });
        }

        public TranslationStatement(string value) : base(StatementKind.Translation)
        {

            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string Value { get; }
    }
}
