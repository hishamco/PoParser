using System;

namespace PoParser.Core.Statements
{
    public class ReferenceSource
    {
        public ReferenceSource(string filePath, int line)
        {
            FilePath = filePath;
            Line = line;
        }

        public string FilePath { get; }

        public int Line { get; }

        public static ReferenceSource Parse(string reference)
        {
            var path = reference;
            var line = 0;
            var index = reference.IndexOf(":");
            if (index > -1)
            {
                path = reference.Substring(0, index);
                line = Convert.ToInt32(reference[(index + 1)..]);
            }

            return new ReferenceSource(path, line);
        }

        public override string ToString()
            => Line > 0
                ? $"{FilePath} : {Line})"
                : FilePath;
    }
}
