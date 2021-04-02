namespace PoParser.Core.Syntax
{
    public class SyntaxNode
    {
        public SyntaxNode(SyntaxToken token)
        {
            Token = token;
        }

        public SyntaxKind Kind => Token.Kind;

        public SyntaxToken Token { get; }
    }
}
