using System;
namespace PoParser.Core
{
    [Flags]
    public enum StatementKind
    {
        None,
        Comment = 0x1,
        MessageId = 0x1,
        PluralMessageId = 0x2,
        Context = 0x4,
        Translation = 0x8,
    }
}
