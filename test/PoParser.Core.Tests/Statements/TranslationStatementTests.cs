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
        public void ParseMultilineValue()
        {
            // Arrange
            var text = @"msgstr """"
""Here is an example of how one might continue a very long string\n""
""for the common case the string represents multi-line output.""";
            var context = new PoParseContext(text);
            var result = new ParseResult<Statement>();

            // Act
            TranslationStatement.Statement.Parse(context, ref result);

            // Assert
            var statement = result.Value as TranslationStatement;
            Assert.Equal(4, statement.Nodes.Count);
            Assert.Equal(SyntaxKind.MessageTranslation, statement.Nodes.ElementAt(0).Kind);
            Assert.Equal("msgstr ", statement.Nodes.ElementAt(0).Token.Value);
            Assert.Equal(SyntaxKind.StringToken, statement.Nodes.ElementAt(1).Kind);
            Assert.Equal(string.Empty, statement.Nodes.ElementAt(1).Token.Value);
            Assert.Equal(SyntaxKind.StringToken, statement.Nodes.ElementAt(2).Kind);
            Assert.Equal("Here is an example of how one might continue a very long string\n", statement.Nodes.ElementAt(2).Token.Value);
            Assert.Equal(SyntaxKind.StringToken, statement.Nodes.ElementAt(3).Kind);
            Assert.Equal("for the common case the string represents multi-line output.", statement.Nodes.ElementAt(3).Token.Value);
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

        [Fact]
        public void GetMultilineValue()
        {
            // Arrange
            var text = @"msgstr """"
""Here is an example of how one might continue a very long string\n""
""for the common case the string represents multi-line output.""";
            var context = new PoParseContext(text);
            var result = new ParseResult<Statement>();

            // Act
            TranslationStatement.Statement.Parse(context, ref result);

            // Assert
            var statement = result.Value as TranslationStatement;

            Assert.Equal("Here is an example of how one might continue a very long string\nfor the common case the string represents multi-line output.", statement.Value);
        }
    }
}
