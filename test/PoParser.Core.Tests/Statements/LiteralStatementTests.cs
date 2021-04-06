using Parlot;
using PoParser.Core.Statements;
using PoParser.Core.Syntax;
using System.Linq;
using Xunit;

namespace PoParser.Core.Tests
{
    public class LiteralStatementTests
    {
        [Fact]
        public void Parse()
        {
            // Arrange
            var text = "\"Plural - Forms: nplurals = 2; plural = n != 1;\n\"";
            var context = new PoParseContext(text);
            var result = new ParseResult<Statement>();

            // Act
            LiteralStatement.Statement.Parse(context, ref result);

            // Assert
            var statement = result.Value as LiteralStatement;
            Assert.Equal(3, statement.Nodes.Count);
            Assert.Equal(SyntaxKind.DoubleQuoteToken, statement.Nodes.ElementAt(0).Kind);
            Assert.Equal("\"", statement.Nodes.ElementAt(0).Token.Value);
            Assert.Equal(SyntaxKind.StringToken, statement.Nodes.ElementAt(1).Kind);
            Assert.Equal("Plural - Forms: nplurals = 2; plural = n != 1;\n", statement.Nodes.ElementAt(1).Token.Value);
            Assert.Equal(SyntaxKind.DoubleQuoteToken, statement.Nodes.ElementAt(2).Kind);
            Assert.Equal("\"", statement.Nodes.ElementAt(2).Token.Value);
        }

        [Fact]
        public void GetText()
        {
            // Arrange
            var text = "\"Plural - Forms: nplurals = 2; plural = n != 1;\n\"";
            var context = new PoParseContext(text);
            var result = new ParseResult<Statement>();

            // Act
            LiteralStatement.Statement.Parse(context, ref result);

            // Assert
            var statement = result.Value as LiteralStatement;

            Assert.Equal("Plural - Forms: nplurals = 2; plural = n != 1;\n", statement.Text);
        }
    }
}
