using Parlot;
using PoParser.Core.Statements;
using System.Linq;
using Xunit;

namespace PoParser.Core.Tests
{
    public class CommentStatementTests
    {
        [Fact]
        public void ParseComment()
        {
            // Arrange
            var text = "# This is a comment";
            var context = new PoParseContext(text);
            var result = new ParseResult<Statement>();

            // Act
            CommentStatement.Statement.Parse(context, ref result);

            // Assert
            var statement = result.Value as CommentStatement;
            Assert.Single(statement.Nodes);
            Assert.Equal(text[2..], statement.Nodes.Single().Token.Value);
        }
    }
}
