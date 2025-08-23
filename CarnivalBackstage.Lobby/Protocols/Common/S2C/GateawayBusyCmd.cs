using CarnivalBackstage.Lobby.Binary;

namespace CarnivalBackstage.Lobby.Protocols.Common.S2C;

internal class GateawayBusyCmd : ISendableCmd
{
    public byte[] Serialize()
    {
        BufferWriter p = new();

        Header h = new(0, 3);
        h.SetBodyLength(0);
        h.Serialize(p);

        return p.ToByteArray();
    }
}
