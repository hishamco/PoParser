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
        public void ParseComment(string comment, string expectedText)
        {
            // Arrange
            var parser = new PoParser();

            // Act
            var statements = parser.Parse(comment);

            // Assert
            Assert.Single(statements);
            Assert.Equal(expectedText, (statements.Single() as CommentStatement).Text);
        }
    }
}
