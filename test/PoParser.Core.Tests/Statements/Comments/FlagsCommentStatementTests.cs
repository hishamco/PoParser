using Parlot;
using System.Linq;
using Xunit;

namespace PoParser.Core.Statements.Tests
{
    public class FlagsCommentStatementTests
    {
        [Fact]
        public void ParseComment()
        {
            // Arrange
            var text = "#, alpha, beta, gamma";
            var context = new PoParseContext(text);
            var result = new ParseResult<Statement>();

            // Act
            FlagsCommentStatement.Statement.Parse(context, ref result);

            // Assert
            var statement = result.Value as CommentStatement;
            Assert.Single(statement.Nodes);
            Assert.Equal(text[3..], statement.Nodes.Single().Token.Value);
        }

        [Fact]
        public void GetFlags()
        {
            // Arrange
            var text = "#, alpha, beta, gamma";
            var context = new PoParseContext(text);
            var result = new ParseResult<Statement>();

            // Act
            FlagsCommentStatement.Statement.Parse(context, ref result);

            // Assert
            var statement = result.Value as FlagsCommentStatement;
            Assert.Equal(3, statement.Flags.Count());
            Assert.Contains("alpha", statement.Flags);
            Assert.Contains("beta", statement.Flags);
            Assert.Contains("gamma", statement.Flags);
        }
    }
}
