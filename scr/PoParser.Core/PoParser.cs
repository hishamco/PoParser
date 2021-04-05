using Parlot.Fluent;
using PoParser.Core.Statements;
using System;
using System.Collections.Generic;
using static Parlot.Fluent.Parsers;

namespace PoParser.Core
{
    public class PoParser : IPoParser
    {
        internal static readonly Parser<string> NewLine = Terms.Text(Environment.NewLine);

        public readonly Deferred<List<Statement>> Grammar = Deferred<List<Statement>>();

        public PoParser()
        {
            var statement = CommentStatement.Statement.
                Or(MessageIdentifierStatement.Statement).
                Or(PluralMessageIdentifierStatement.Statement).
                Or(MessageContextStatement.Statement);

            Grammar.Parser = Separated(NewLine, statement);
        }

        public IEnumerable<Statement> Parse(string content)
        {
            Grammar.TryParse(content, out List<Statement> statements);

            return statements;
        }
    }
}
