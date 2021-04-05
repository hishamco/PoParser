using PoParser.Core.Statements;
using System.Linq;
using Xunit;

namespace PoParser.Core.Tests
{
    public class PoParserTests
    {
        [Theory]
        [InlineData("#. TRANSLATORS: A test phrase with all letters of the English alphabet.", "TRANSLATORS: A test phrase with all letters of the English alphabet.")]
        [InlineData("#, alpha, beta, gamma", "alpha, beta, gamma")]
        [InlineData("#~ msgid \"Set the telescope longitude and latitude.\"", "msgid \"Set the telescope longitude and latitude.\"")]
        [InlineData("#| msgid \"Solar System\"", "msgid \"Solar System\"")]
        [InlineData("#: lib/Class1.cs:45 lib/Class2.cs", "lib/Class1.cs:45 lib/Class2.cs")]
        [InlineData("# This is a comment", "This is a comment")]
        public void ParseComment(string line, string expectedText)
        {
            // Arrange
            var parser = new PoParser();

            // Act
            var statements = parser.Parse(line);

            // Assert
            Assert.Single(statements);
            Assert.Equal(expectedText, (statements.Single() as CommentStatement).Text);
        }

        [Theory]
        [InlineData("msgid \"error\"", "error")]
        [InlineData("msgid \"Unknown system error\"", "Unknown system error")]
        public void ParseMessageIdentifier(string line, string expectedTranslation)
        {
            // Arrange
            var parser = new PoParser();

            // Act
            var statements = parser.Parse(line);

            // Assert
            Assert.Single(statements);
            Assert.Equal(expectedTranslation, (statements.Single() as MessageIdentifierStatement).Identifier);
        }

        [Theory]
        [InlineData("msgid_plural \"{0} hours to midnight\"", "{0} hours to midnight")]
        public void ParsePluralMessageIdentifier(string line, string expectedTranslation)
        {
            // Arrange
            var parser = new PoParser();

            // Act
            var statements = parser.Parse(line);

            // Assert
            Assert.Single(statements);
            Assert.Equal(expectedTranslation, (statements.Single() as PluralMessageIdentifierStatement).Identifier);
        }

        [Theory]
        [InlineData("msgctxt \"\"", "")]
        [InlineData("msgctxt \"Context id of\na long text\"", "Context id of\na long text")]
        public void ParseMessageContext(string line, string expectedTranslation)
        {
            // Arrange
            var parser = new PoParser();

            // Act
            var statements = parser.Parse(line);

            // Assert
            Assert.Single(statements);
            Assert.Equal(expectedTranslation, (statements.Single() as MessageContextStatement).Context);
        }

        [Theory]
        [InlineData("msgstr \"\"", "")]
        [InlineData("msgstr \"Hello\"", "Hello")]
        [InlineData("msgstr \"Translation of\na long text\"", "Translation of\na long text")]
        public void ParseTranslation(string line, string expectedTranslation)
        {
            // Arrange
            var parser = new PoParser();

            // Act
            var statements = parser.Parse(line);

            // Assert
            Assert.Single(statements);
            Assert.Equal(expectedTranslation, (statements.Single() as TranslationStatement).Value);
        }

        [Theory]
        [InlineData("msgstr[1] \"Plural translation of\na long text\"", 1, "Plural translation of\na long text")]
        public void ParsePluralTranslation(string line, int expectedTranslationIndex, string expectedTranslationValue)
        {
            // Arrange
            var parser = new PoParser();

            // Act
            var statements = parser.Parse(line);

            // Assert
            Assert.Single(statements);
            Assert.Equal(expectedTranslationIndex, (statements.Single() as PluralTranslationStatement).Index);
            Assert.Equal(expectedTranslationValue, (statements.Single() as PluralTranslationStatement).Value);
        }
    }
}
