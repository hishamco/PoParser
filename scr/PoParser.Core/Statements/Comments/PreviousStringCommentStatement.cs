namespace PoParser.Core.Statements
{
    public class PreviousStringCommentStatement : CommentStatement
    {
        public PreviousStringCommentStatement(string text) : base(CommentKind.PreviousValue, text)
        {
        }
    }
}
