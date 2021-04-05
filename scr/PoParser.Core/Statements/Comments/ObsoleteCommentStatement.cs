namespace PoParser.Core.Statements
{
    public class ObsoleteCommentStatement : CommentStatement
    {
        public ObsoleteCommentStatement(string text) : base(CommentKind.Obsolete, text)
        {
        }
    }
}
