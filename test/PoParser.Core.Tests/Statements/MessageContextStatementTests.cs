using Parlot;
using PoParser.Core.Statements;
using PoParser.Core.Syntax;
using System.Linq;
using Xunit;

namespace PoParser.Core.Tests
{
    public class MessageContextStatementTests
    {
        [Fact]
        public void Parse()
        {
            // Arrange
            var text = "msgctxt \"Context id of\na long text\"";
            var context = new PoParseContext(text);
            var result = new ParseResult<Statement>();

            // Act
            MessageContextStatement.Statement.Parse(context, ref result);

            // Assert
            var statement = result.Value as MessageContextStatement;
            Assert.Equal(4, statement.Nodes.Count);
            Assert.Equal(SyntaxKind.IdentifierToken, statement.Nodes.ElementAt(0).Kind);
            Assert.Equal("msgctxt", statement.Nodes.ElementAt(0).Token.Value);
            Assert.Equal(SyntaxKind.DoubleQuoteToken, statement.Nodes.ElementAt(1).Kind);
            Assert.Equal("\"", statement.Nodes.ElementAt(1).Token.Value);
            Assert.Equal(SyntaxKind.StringToken, statement.Nodes.ElementAt(2).Kind);
            Assert.Equal("Context id of\na long text", statement.Nodes.ElementAt(2).Token.Value);
            Assert.Equal(SyntaxKind.DoubleQuoteToken, statement.Nodes.ElementAt(3).Kind);
            Assert.Equal("\"", statement.Nodes.ElementAt(3).Token.Value);
        }

        [Fact]
        public void GetContext()
        {
            // Arrange
            var text = "msgctxt \"Context id of\na long text\"";
            var context = new PoParseContext(text);
            var result = new ParseResult<Statement>();

            // Act
            MessageContextStatement.Statement.Parse(context, ref result);

            // Assert
            var statement = result.Value as MessageContextStatement;

            Assert.Equal("Context id of\na long text", statement.Context);
        }
    }
}
