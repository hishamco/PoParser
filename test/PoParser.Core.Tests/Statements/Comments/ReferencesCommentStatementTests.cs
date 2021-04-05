using Parlot;
using System.Linq;
using Xunit;

namespace PoParser.Core.Statements.Tests
{
    public class ReferencesCommentStatementTests
    {
        [Fact]
        public void ParseComment()
        {
            // Arrange
            var text = "#: lib/Class1.cs:45 lib/Class2.cs";
            var context = new PoParseContext(text);
            var result = new ParseResult<Statement>();

            // Act
            ReferencesCommentStatement.Statement.Parse(context, ref result);

            // Assert
            var statement = result.Value as CommentStatement;
            Assert.Single(statement.Nodes);
            Assert.Equal(text[3..], statement.Nodes.Single().Token.Value);
        }

        [Fact]
        public void GetReferences()
        {
            // Arrange
            var text = "#: lib/Class1.cs:45 lib/Class2.cs";
            var context = new PoParseContext(text);
            var result = new ParseResult<Statement>();

            // Act
            ReferencesCommentStatement.Statement.Parse(context, ref result);

            // Assert
            var statement = result.Value as ReferencesCommentStatement;
            Assert.Equal(2, statement.References.Count());
            Assert.Equal("lib/Class1.cs", statement.References.ElementAt(0).FilePath);
            Assert.Equal(45, statement.References.ElementAt(0).Line);
            Assert.Equal("lib/Class2.cs", statement.References.ElementAt(1).FilePath);
            Assert.Equal(0, statement.References.ElementAt(1).Line);
        }
    }
}
