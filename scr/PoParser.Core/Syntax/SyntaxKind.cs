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
        StringToken,
        HashToken,
        ColonToken,
        CommaToken,
        TeldaToken,
        PipeToken,
        DotToken,

        // Statements
        CommentStatement
    }
}