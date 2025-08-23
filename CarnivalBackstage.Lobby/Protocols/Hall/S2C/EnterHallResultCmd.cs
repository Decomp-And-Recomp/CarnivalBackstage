using CarnivalBackstage.Lobby.Binary;

namespace CarnivalBackstage.Lobby.Protocols.Hall.S2C;

internal class EnterHallResultCmd : ISendableCmd
{
    public enum Code
    { 
        kOk = 0,
        kNoChange = 1
    }

    readonly Code code;

    public EnterHallResultCmd(Code code)
    {
        this.code = code;
    }

    public byte[] Serialize()
    {
        BufferWriter p = new();

        Header h = new(3, 1);
        h.SetBodyLength(1);
        h.Serialize(p);

        p.PushByte((byte)code);

        return p.ToByteArray();
    }
}
