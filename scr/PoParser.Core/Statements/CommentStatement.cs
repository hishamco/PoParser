using Parlot.Fluent;
using PoParser.Core.Syntax;
using System;
using static Parlot.Fluent.Parsers;

namespace PoParser.Core.Statements
{
    public abstract class CommentStatement : Statement
    {
        public static readonly Deferred<Statement> Statement = Deferred<Statement>();

        static CommentStatement()
        {
            var hashNode = Terms.Char('#').
                Then(c => new SyntaxNode(new SyntaxToken(SyntaxKind.HashToken, c.ToString())));
            var commaNode = Terms.Char(',').
                Then(c => new SyntaxNode(new SyntaxToken(SyntaxKind.CommaToken, c.ToString())));
            var teldaNode = Terms.Char('~').
                Then(c => new SyntaxNode(new SyntaxToken(SyntaxKind.TeldaToken, c.ToString())));
            var pipeNode = Terms.Char('|').
                Then(c => new SyntaxNode(new SyntaxToken(SyntaxKind.PipeToken, c.ToString())));
            var colonNode = Terms.Char(':').
                Then(c => new SyntaxNode(new SyntaxToken(SyntaxKind.ColonToken, c.ToString())));
            var dotNode = Terms.Char('.').
                Then(c => new SyntaxNode(new SyntaxToken(SyntaxKind.DotToken, c.ToString())));
            var commentStatement = hashNode.And(ZeroOrOne(commaNode.Or(teldaNode).Or(pipeNode).Or(colonNode).Or(dotNode))).
                And(Terms.Pattern(p => true).Then(e => new SyntaxNode(new SyntaxToken(SyntaxKind.StringToken, e.ToString()))));
            Statement.Parser = commentStatement
                .Then<Statement>(e =>
                {
                    CommentStatement statement;
                    var commentText = e.Item3.Token.Value.ToString();
                    var node = new SyntaxNode(new SyntaxToken(SyntaxKind.CommentTrivia, commentText));

                    statement = e.Item2?.Kind switch
                    {
                        SyntaxKind.CommaToken => new FlagsCommentStatement(commentText),
                        SyntaxKind.PipeToken => new PreviousStringCommentStatement(commentText),
                        SyntaxKind.ColonToken => new ReferencesCommentStatement(commentText),
                        SyntaxKind.DotToken => new ExtractedCommentStatement(commentText),
                        SyntaxKind.TeldaToken => new ObsoleteCommentStatement(commentText),
                        _ => new TranslatorCommentStatement(commentText)
                    };

                    statement.Nodes.Add(node);

                    return statement;
                });
        }

        public CommentStatement(CommentKind kind, string text)
        {
            if (kind == CommentKind.None)
            {
                throw new ArgumentException(null, nameof(kind));
            }

            Kind = kind;
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }

        public CommentKind Kind { get; }

        public string Text { get; }
    }
}
