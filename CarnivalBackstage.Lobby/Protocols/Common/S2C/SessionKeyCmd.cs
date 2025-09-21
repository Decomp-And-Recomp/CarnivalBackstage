using CarnivalBackstage.Lobby.Binary;

namespace CarnivalBackstage.Lobby.Protocols.Common.S2C;

internal class SessionKeyCmd : ISendableCmd
{
    readonly byte[] key;

    public SessionKeyCmd(byte[] key)
    {
        this.key = key;
    }

    public byte[] Serialize()
    {
        BufferWriter p = new();

        Header h = new(0, (byte)Cmd.session_key_cs);
        h.SetBodyLength(key.Length + 2);
        h.Serialize(p);

        p.PushByteArray(key);
        p.PushByte(0);
        p.PushByte(87);

        return p.ToByteArray();
    }
}
