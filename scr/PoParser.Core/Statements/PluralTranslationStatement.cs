using Parlot.Fluent;
using PoParser.Core.Syntax;
using System;
using static Parlot.Fluent.Parsers;

namespace PoParser.Core.Statements
{
    public class PluralTranslationStatement : Statement
    {
        public static readonly Deferred<Statement> Statement = Deferred<Statement>();

        static PluralTranslationStatement()
        {
            var messageTranslationNode = Terms.Text("msgstr").
                Then(c => new SyntaxNode(new SyntaxToken(SyntaxKind.MessageTranslation, c)));
            var leftBracketNode = Terms.Char('[').
                Then(c => new SyntaxNode(new SyntaxToken(SyntaxKind.LeftBracketToken, c.ToString())));
            var indexNode = Terms.Integer().
                Then(c => new SyntaxNode(new SyntaxToken(SyntaxKind.NumberToken, c)));
            var rightBracketNode = Terms.Text("] ").
                Then(c => new SyntaxNode(new SyntaxToken(SyntaxKind.RightBracketToken, c.TrimEnd())));
            var translationNode = Terms.String(StringLiteralQuotes.Double).
                Then(c => new SyntaxNode(new SyntaxToken(SyntaxKind.StringToken, c.ToString())));
            var translationStatement = messageTranslationNode.And(leftBracketNode).And(indexNode).And(rightBracketNode).And(translationNode);

            Statement.Parser = translationStatement
                .Then<Statement>(e =>
                {
                    var statement = new PluralTranslationStatement(Convert.ToInt32(e.Item3.Token.Value), e.Item5.Token.Value.ToString());

                    statement.Nodes.Add(e.Item1);
                    statement.Nodes.Add(e.Item2);
                    statement.Nodes.Add(e.Item3);
                    statement.Nodes.Add(e.Item4);
                    statement.Nodes.Add(e.Item5);

                    return statement;
                });
        }

        public PluralTranslationStatement(int index, string value) : base(StatementKind.Translation)
        {
            Index = index;
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public int Index { get; }

        public string Value { get; }
    }
}
