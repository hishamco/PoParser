using Parlot.Fluent;
using PoParser.Core.Syntax;
using System;
using static Parlot.Fluent.Parsers;

namespace PoParser.Core.Statements
{
    public class TranslationStatement : Statement
    {
        public static readonly Deferred<Statement> Statement = Deferred<Statement>();

        static TranslationStatement()
        {
            var messageTranslationNode = Terms.Text("msgstr ").
                Then(c => new SyntaxNode(new SyntaxToken(SyntaxKind.MessageTranslation, c)));
            var translationNode = Terms.String(StringLiteralQuotes.Double).
                Then(c => new SyntaxNode(new SyntaxToken(SyntaxKind.StringToken, c.ToString())));
            var translationStatement = messageTranslationNode.And(translationNode);

            Statement.Parser = translationStatement
                .Then<Statement>(e =>
                {
                    var statement = new TranslationStatement(e.Item2.Token.Value.ToString());

                    statement.Nodes.Add(e.Item1);
                    statement.Nodes.Add(e.Item2);

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
