using CarnivalBackstage.Lobby.Binary;

namespace CarnivalBackstage.Lobby.Protocols.Role.S2C;

internal class RegisterResultCmd : ISendableCmd
{
    public enum Code
    {
        kOk = 0,
        kError = 1
    }

    readonly Code code;

    public RegisterResultCmd(Code code)
    {
        this.code = code;
    }

    public byte[] Serialize()
    {
        BufferWriter p = new();

        Header h = new(1, 12);
        h.SetBodyLength(1);
        h.Serialize(p);

        p.PushByte((byte)code);

        return p.ToByteArray();
    }
}
