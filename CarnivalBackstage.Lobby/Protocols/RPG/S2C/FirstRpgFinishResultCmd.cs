using CarnivalBackstage.Lobby.Binary;

namespace CarnivalBackstage.Lobby.Protocols.RPG.S2C;

internal class FirstRpgFinishResultCmd : ISendableCmd
{
    public byte[] Serialize()
    {
        BufferWriter w = new();
        Header h = new(7, 62);
        h.SetBodyLength(0);
        h.Serialize(w);

        return w.ToByteArray();
    }
}
