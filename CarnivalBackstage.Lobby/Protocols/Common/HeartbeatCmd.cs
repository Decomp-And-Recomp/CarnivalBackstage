using CarnivalBackstage.Lobby.Binary;

namespace CarnivalBackstage.Lobby.Protocols.Common;

internal class HeartbeatCmd : ISendableCmd
{
    public byte[] Serialize()
    {
        BufferWriter w = new();
        Header h = new(0, 0);
        h.SetBodyLength(0);
        h.Serialize(w);

        return w.ToByteArray();
    }
}
