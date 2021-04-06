using Parlot;
using PoParser.Core.Statements;
using PoParser.Core.Syntax;
using System.Linq;
using Xunit;

namespace PoParser.Core.Tests
{
    public class PluralMessageIdentifierStatementTests
    {
        [Fact]
        public void Parse()
        {
            // Arrange
            var text = "msgid_plural \"{0} hours to midnight\"";
            var context = new PoParseContext(text);
            var result = new ParseResult<Statement>();

            // Act
            PluralMessageIdentifierStatement.Statement.Parse(context, ref result);

            // Assert
            var statement = result.Value as PluralMessageIdentifierStatement;
            Assert.Equal(4, statement.Nodes.Count);
            Assert.Equal(SyntaxKind.IdentifierToken, statement.Nodes.ElementAt(0).Kind);
            Assert.Equal("msgid_plural", statement.Nodes.ElementAt(0).Token.Value);
            Assert.Equal(SyntaxKind.DoubleQuoteToken, statement.Nodes.ElementAt(1).Kind);
            Assert.Equal("\"", statement.Nodes.ElementAt(1).Token.Value);
            Assert.Equal(SyntaxKind.StringToken, statement.Nodes.ElementAt(2).Kind);
            Assert.Equal("{0} hours to midnight", statement.Nodes.ElementAt(2).Token.Value);
            Assert.Equal(SyntaxKind.DoubleQuoteToken, statement.Nodes.ElementAt(3).Kind);
            Assert.Equal("\"", statement.Nodes.ElementAt(3).Token.Value);
        }

        [Fact]
        public void GetIdentifier()
        {
            // Arrange
            var text = "msgid_plural \"{0} hours to midnight\"";
            var context = new PoParseContext(text);
            var result = new ParseResult<Statement>();

            // Act
            PluralMessageIdentifierStatement.Statement.Parse(context, ref result);

            // Assert
            var statement = result.Value as PluralMessageIdentifierStatement;

            Assert.Equal("{0} hours to midnight", statement.Identifier);
        }
    }
}
