using Parlot;
using PoParser.Core.Statements;
using PoParser.Core.Syntax;
using System.Linq;
using Xunit;

namespace PoParser.Core.Tests
{
    public class MessageIdentifierStatementTests
    {
        [Fact]
        public void Parse()
        {
            // Arrange
            var text = "msgid \"Unknown system error\"";
            var context = new PoParseContext(text);
            var result = new ParseResult<Statement>();

            // Act
            MessageIdentifierStatement.Statement.Parse(context, ref result);

            // Assert
            var statement = result.Value as MessageIdentifierStatement;
            Assert.Equal(4, statement.Nodes.Count);
            Assert.Equal(SyntaxKind.IdentifierToken, statement.Nodes.ElementAt(0).Kind);
            Assert.Equal("msgid", statement.Nodes.ElementAt(0).Token.Value);
            Assert.Equal(SyntaxKind.DoubleQuoteToken, statement.Nodes.ElementAt(1).Kind);
            Assert.Equal("\"", statement.Nodes.ElementAt(1).Token.Value);
            Assert.Equal(SyntaxKind.StringToken, statement.Nodes.ElementAt(2).Kind);
            Assert.Equal("Unknown system error", statement.Nodes.ElementAt(2).Token.Value);
            Assert.Equal(SyntaxKind.DoubleQuoteToken, statement.Nodes.ElementAt(3).Kind);
            Assert.Equal("\"", statement.Nodes.ElementAt(3).Token.Value);
        }

        [Fact]
        public void ParseMultilineIdentifier()
        {
            // Arrange
            var text = @"msgid """"
""Here is an example of how one might continue a very long string\n""
""for the common case the string represents multi-line output.""";
            var context = new PoParseContext(text);
            var result = new ParseResult<Statement>();

            // Act
            MessageIdentifierStatement.Statement.Parse(context, ref result);

            // Assert
            var statement = result.Value as MessageIdentifierStatement;
            Assert.Equal(4, statement.Nodes.Count);
            Assert.Equal(SyntaxKind.IdentifierToken, statement.Nodes.ElementAt(0).Kind);
            Assert.Equal("msgid", statement.Nodes.ElementAt(0).Token.Value);
            Assert.Equal(SyntaxKind.DoubleQuoteToken, statement.Nodes.ElementAt(1).Kind);
            Assert.Equal("\"", statement.Nodes.ElementAt(1).Token.Value);
            Assert.Equal(SyntaxKind.StringToken, statement.Nodes.ElementAt(2).Kind);
            Assert.Equal("Here is an example of how one might continue a very long string\nfor the common case the string represents multi-line output.", statement.Nodes.ElementAt(2).Token.Value);
            Assert.Equal(SyntaxKind.DoubleQuoteToken, statement.Nodes.ElementAt(3).Kind);
            Assert.Equal("\"", statement.Nodes.ElementAt(3).Token.Value);
        }

        [Fact]
        public void GetIdentifier()
        {
            // Arrange
            var text = "msgid \"Unknown system error\"";
            var context = new PoParseContext(text);
            var result = new ParseResult<Statement>();

            // Act
            MessageIdentifierStatement.Statement.Parse(context, ref result);

            // Assert
            var statement = result.Value as MessageIdentifierStatement;

            Assert.Equal("Unknown system error", statement.Identifier);
        }

        [Fact]
        public void GetMultilineIdentifier()
        {
            // Arrange
            var text = @"msgid """"
""Here is an example of how one might continue a very long string\n""
""for the common case the string represents multi-line output.""";
            var context = new PoParseContext(text);
            var result = new ParseResult<Statement>();

            // Act
            MessageIdentifierStatement.Statement.Parse(context, ref result);

            // Assert
            var statement = result.Value as MessageIdentifierStatement;

            Assert.Equal("Here is an example of how one might continue a very long string\nfor the common case the string represents multi-line output.", statement.Identifier);
        }
    }
}
