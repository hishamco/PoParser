namespace PoParser.Core.Statements
{
    public class TranslatorCommentStatement : CommentStatement
    {
        public TranslatorCommentStatement(string text) : base(CommentKind.Translator, text)
        {
        }
    }
}
