using Parlot;
using PoParser.Core.Statements;
using PoParser.Core.Syntax;
using System.Linq;
using Xunit;

namespace PoParser.Core.Tests
{
    public class TranslationStatementTests
    {
        [Fact]
        public void Parse()
        {
            // Arrange
            var text = "msgstr \"Translation of\na long text\"";
            var context = new PoParseContext(text);
            var result = new ParseResult<Statement>();

            // Act
            TranslationStatement.Statement.Parse(context, ref result);

            // Assert
            var statement = result.Value as TranslationStatement;
            Assert.Equal(2, statement.Nodes.Count);
            Assert.Equal(SyntaxKind.MessageTranslation, statement.Nodes.ElementAt(0).Kind);
            Assert.Equal("msgstr ", statement.Nodes.ElementAt(0).Token.Value);
            Assert.Equal(SyntaxKind.StringToken, statement.Nodes.ElementAt(1).Kind);
            Assert.Equal("Translation of\na long text", statement.Nodes.ElementAt(1).Token.Value);
        }

        [Fact]
        public void GetValue()
        {
            // Arrange
            var text = "msgstr \"Translation of\na long text\"";
            var context = new PoParseContext(text);
            var result = new ParseResult<Statement>();

            // Act
            TranslationStatement.Statement.Parse(context, ref result);

            // Assert
            var statement = result.Value as TranslationStatement;

            Assert.Equal("Translation of\na long text", statement.Value);
        }
    }
}
