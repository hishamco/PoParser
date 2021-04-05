using Parlot;
using PoParser.Core.Statements;
using PoParser.Core.Syntax;
using System.Linq;
using Xunit;

namespace PoParser.Core.Tests
{
    public class PluralTranslationStatementTests
    {
        [Fact]
        public void Parse()
        {
            // Arrange
            var text = "msgstr[1] \"Plural translation of\na long text\"";
            var context = new PoParseContext(text);
            var result = new ParseResult<Statement>();

            // Act
            PluralTranslationStatement.Statement.Parse(context, ref result);

            // Assert
            var statement = result.Value as PluralTranslationStatement;
            Assert.Equal(5, statement.Nodes.Count);
            Assert.Equal(SyntaxKind.MessageTranslation, statement.Nodes.ElementAt(0).Kind);
            Assert.Equal("msgstr", statement.Nodes.ElementAt(0).Token.Value);
            Assert.Equal(SyntaxKind.LeftBracketToken, statement.Nodes.ElementAt(1).Kind);
            Assert.Equal("[", statement.Nodes.ElementAt(1).Token.Value);
            Assert.Equal(SyntaxKind.NumberToken, statement.Nodes.ElementAt(2).Kind);
            Assert.Equal(1L, statement.Nodes.ElementAt(2).Token.Value);
            Assert.Equal(SyntaxKind.RightBracketToken, statement.Nodes.ElementAt(3).Kind);
            Assert.Equal("]", statement.Nodes.ElementAt(3).Token.Value);
            Assert.Equal(SyntaxKind.StringToken, statement.Nodes.ElementAt(4).Kind);
            Assert.Equal("Plural translation of\na long text", statement.Nodes.ElementAt(4).Token.Value);
        }

        [Fact]
        public void GetIndexAndValue()
        {
            // Arrange
            var text = "msgstr[1] \"Plural translation of\na long text\"";
            var context = new PoParseContext(text);
            var result = new ParseResult<Statement>();

            // Act
            PluralTranslationStatement.Statement.Parse(context, ref result);

            // Assert
            var statement = result.Value as PluralTranslationStatement;

            Assert.Equal(1, statement.Index);
            Assert.Equal("Plural translation of\na long text", statement.Value);
        }
    }
}
