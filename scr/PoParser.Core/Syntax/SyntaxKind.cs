namespace PoParser.Core.Syntax
{
    public enum SyntaxKind
    {
        BadToken,

        // Trivia
        LineBreakTrivia,
        WhitespaceTrivia,
        CommentTrivia,

        // Tokens

        // Statements
        CommentStatement
    }
}