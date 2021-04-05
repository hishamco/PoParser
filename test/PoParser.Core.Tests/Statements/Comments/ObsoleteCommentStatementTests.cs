using Parlot;
using PoParser.Core.Statements;
using System.Linq;
using Xunit;

namespace PoParser.Core.Tests
{
    public class ObsoleteCommentStatementTests
    {
        [Fact]
        public void ParseComment()
        {
            // Arrange
            var text = "#~ msgid \"Set the telescope longitude and latitude.\"";
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
