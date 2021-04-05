namespace PoParser.Core.Statements
{
    public class ExtractedCommentStatement : CommentStatement
    {
        public ExtractedCommentStatement(string text) : base(CommentKind.Extracted, text)
        {
        }
    }
}
