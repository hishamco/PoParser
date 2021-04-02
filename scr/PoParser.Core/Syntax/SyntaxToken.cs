namespace PoParser.Core.Syntax
{
    public struct SyntaxToken
    {
        public SyntaxToken(SyntaxKind kind, object? value)
        {
            Kind = kind;
            Value = value;
        }

        public SyntaxKind Kind { get; }

        public object Value { get; }
    }
}
