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
        NumberToken,
        HashToken,
        ColonToken,
        CommaToken,
        TeldaToken,
        PipeToken,
        DotToken,
        DoubleQuoteToken,
        LeftBracketToken,
        RightBracketToken,
        MessageIdentifierToken,
        MessageContextToken,
        MessageTranslation
    }
}