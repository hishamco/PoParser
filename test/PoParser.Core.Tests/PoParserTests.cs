using PoParser.Core.Statements;
using PoParser.Core.Syntax;
using System;
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
            var tree = parser.Parse(line);

            // Assert
            Assert.Single(tree.Statements);
            Assert.Equal(expectedText, (tree.Statements.Single() as CommentStatement).Text);
        }

        [Theory]
        [InlineData("msgid \"error\"", "error")]
        [InlineData("msgid \"Unknown system error\"", "Unknown system error")]
        public void ParseMessageIdentifier(string line, string expectedTranslation)
        {
            // Arrange
            var parser = new PoParser();

            // Act
            var tree = parser.Parse(line);

            // Assert
            Assert.Single(tree.Statements);
            Assert.Equal(expectedTranslation, (tree.Statements.Single() as MessageIdentifierStatement).Identifier);
        }

        [Theory]
        [InlineData("msgid_plural \"{0} hours to midnight\"", "{0} hours to midnight")]
        public void ParsePluralMessageIdentifier(string line, string expectedTranslation)
        {
            // Arrange
            var parser = new PoParser();

            // Act
            var tree = parser.Parse(line);

            // Assert
            Assert.Single(tree.Statements);
            Assert.Equal(expectedTranslation, (tree.Statements.Single() as PluralMessageIdentifierStatement).Identifier);
        }

        [Theory]
        [InlineData("msgctxt \"\"", "")]
        [InlineData("msgctxt \"Context id of\na long text\"", "Context id of\na long text")]
        public void ParseMessageContext(string line, string expectedTranslation)
        {
            // Arrange
            var parser = new PoParser();

            // Act
            var tree = parser.Parse(line);

            // Assert
            Assert.Single(tree.Statements);
            Assert.Equal(expectedTranslation, (tree.Statements.Single() as MessageContextStatement).Context);
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
            var tree = parser.Parse(line);

            // Assert
            Assert.Single(tree.Statements);
            Assert.Equal(expectedTranslation, (tree.Statements.Single() as TranslationStatement).Value);
        }

        [Theory]
        [InlineData("msgstr[1] \"Plural translation of\na long text\"", 1, "Plural translation of\na long text")]
        public void ParsePluralTranslation(string line, int expectedTranslationIndex, string expectedTranslationValue)
        {
            // Arrange
            var parser = new PoParser();

            // Act
            var tree = parser.Parse(line);

            // Assert
            Assert.Single(tree.Statements);
            Assert.Equal(expectedTranslationIndex, (tree.Statements.Single() as PluralTranslationStatement).Index);
            Assert.Equal(expectedTranslationValue, (tree.Statements.Single() as PluralTranslationStatement).Value);
        }

        [Theory]
        [InlineData("msgctxt \"Home\"", 1, new StatementKind[] { StatementKind.Context })]
        [InlineData("msgctxt \"Home\"\r\nmsgid \"Id of\na long text\"", 2, new StatementKind[] { StatementKind.Context, StatementKind.MessageId })]
        [InlineData(@"#  translator-comments
#. extracted-comments
#: reference…
#, flag
msgid ""Id of\na long text""
msgid_plural ""Plural id of\na long text""
msgstr[0] ""Singular translation of\na long text""
msgstr[1] ""Plural translation of\na long text""", 8, new StatementKind[] { StatementKind.Comment, StatementKind.Comment, StatementKind.Comment, StatementKind.Comment, StatementKind.MessageId, StatementKind.PluralMessageId, StatementKind.Translation, StatementKind.Translation })]
        public void ParseMultiplePOLines(string lines, int expectedStatementsNo, StatementKind[] expectedStatementsKind)
        {
            // Arrange
            var parser = new PoParser();

            // Act
            var syntaxTree = parser.Parse(lines);

            // Assert
            Assert.True(syntaxTree.Statements.Count() == expectedStatementsNo);

            for (int i = 0; i < expectedStatementsKind.Length; i++)
            {
                Assert.Equal(expectedStatementsKind[i], syntaxTree.Statements[i].Kind);
            }
        }
    }
}
