using System;

namespace Castle.Common.Facilities.EventWiring
{
    internal enum OpCodeValues : ushort
    {
        Nop = 0x0000,
        Ldarg_0 = 0x0002,
        Ldarg_1 = 0x0003,
        Ldnull = 0x0014,
        Call = 0x0028,
        Ret = 0x002A,
        Callvirt = 0x006F
    }
}