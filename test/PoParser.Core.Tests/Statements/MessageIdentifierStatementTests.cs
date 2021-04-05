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
            Assert.Equal(2, statement.Nodes.Count);
            Assert.Equal(SyntaxKind.MessageIdentifierToken, statement.Nodes.ElementAt(0).Kind);
            Assert.Equal("msgid ", statement.Nodes.ElementAt(0).Token.Value);
            Assert.Equal(SyntaxKind.StringToken, statement.Nodes.ElementAt(1).Kind);
            Assert.Equal("Unknown system error", statement.Nodes.ElementAt(1).Token.Value);
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
    }
}
