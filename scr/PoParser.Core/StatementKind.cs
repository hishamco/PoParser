using System;
namespace PoParser.Core
{
    [Flags]
    public enum StatementKind
    {
        None,
        Comment = 0x1,
        MessageId = 0x2,
        PluralMessageId = 0x4,
        Context = 0x8,
        Translation = 0x16,
    }
}
