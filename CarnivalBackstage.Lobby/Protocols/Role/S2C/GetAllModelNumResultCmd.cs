using CarnivalBackstage.Lobby.Binary;

namespace CarnivalBackstage.Lobby.Protocols.Role.S2C;

internal class GetAllModelNumResultCmd : ISendableCmd
{
    public byte[] Serialize()
    {
        BufferWriter p = new();

        Header h = new(1, 77);
        h.SetBodyLength(1);
        h.Serialize(p);

        p.PushByte(0);

        return p.ToByteArray();
    }
}
