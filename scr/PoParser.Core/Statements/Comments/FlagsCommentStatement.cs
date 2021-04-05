using System;
using System.Collections.Generic;
using System.Linq;

namespace PoParser.Core.Statements
{
    public class FlagsCommentStatement : CommentStatement
    {
        private static readonly char[] _separator = new[] { ',' };

        public FlagsCommentStatement(string text) : base(CommentKind.Flags, text)
        {
            Flags = text
                .Split(_separator, StringSplitOptions.RemoveEmptyEntries)
                .Select(p => p.Trim());
        }

        public IEnumerable<string> Flags { get; }

        public override string ToString()
            => Flags != null
                ? string.Join(", ", Flags)
                : string.Empty;
    }
}
