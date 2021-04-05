using Parlot;
using PoParser.Core.Statements;
using System.Linq;
using Xunit;

namespace PoParser.Core.Tests
{
    public class ExtractedCommentStatementTests
    {
        [Fact]
        public void ParseComment()
        {
            // Arrange
            var text = "#. TRANSLATORS: A test phrase with all letters of the English alphabet.";
            var context = new PoParseContext(text);
            var result = new ParseResult<Statement>();

            // Act
            ExtractedCommentStatement.Statement.Parse(context, ref result);

            // Assert
            var statement = result.Value as CommentStatement;
            Assert.Single(statement.Nodes);
            Assert.Equal(text[3..], statement.Nodes.Single().Token.Value);
        }
    }
}
