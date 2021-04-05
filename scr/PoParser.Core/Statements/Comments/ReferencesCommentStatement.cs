using System;
using System.Collections.Generic;

namespace PoParser.Core.Statements
{
    public class ReferencesCommentStatement : CommentStatement
    {
        public ReferencesCommentStatement(string text) : base(CommentKind.Reference, text)
        {
            References = Parse(Text);
        }

        public IEnumerable<ReferenceSource> References { get; }

        private static IEnumerable<ReferenceSource> Parse(string text)
        {
            var references = new List<ReferenceSource>();
            foreach (var reference in text.Split(" ", StringSplitOptions.RemoveEmptyEntries))
            {
                references.Add(ReferenceSource.Parse(reference));
            }

            return references;
        }
    }
}
