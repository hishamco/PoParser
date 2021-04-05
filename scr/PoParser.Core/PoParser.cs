using Parlot.Fluent;
using PoParser.Core.Statements;
using PoParser.Core.Syntax;
using System.Collections.Generic;
using static Parlot.Fluent.Parsers;

namespace PoParser.Core
{
    public class PoParser : IPoParser
    {
        public readonly Deferred<List<Statement>> Grammar = Deferred<List<Statement>>();

        public PoParser()
        {
            var statement = CommentStatement.Statement.
                Or(MessageIdentifierStatement.Statement).
                Or(PluralMessageIdentifierStatement.Statement).
                Or(MessageContextStatement.Statement).
                Or(TranslationStatement.Statement).
                Or(PluralTranslationStatement.Statement);

            Grammar.Parser = OneOrMany(statement);
        }

        public SyntaxTree Parse(string content)
        {
            Grammar.TryParse(content, out List<Statement> statements);

            var syntaxTree = new SyntaxTree();
            if (statements != null)
            {
                syntaxTree.Statements.AddRange(statements);
            }

            return syntaxTree;
        }
    }
}
