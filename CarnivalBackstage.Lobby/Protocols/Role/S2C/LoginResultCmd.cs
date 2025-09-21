using CarnivalBackstage.Lobby.Binary;

namespace CarnivalBackstage.Lobby.Protocols.Role.S2C;

internal class LoginResultCmd : ISendableCmd
{
    public enum Code : byte
    {
        kOk = 0,
        kNeedRegister = 1,
        kFull = 2
    }

    readonly Code code;

    public LoginResultCmd(Code code)
    {
        this.code = code;
    }

    public byte[] Serialize()
    {
        BufferWriter p = new();

        Header h = new((byte)ProtoID.ROLE, (byte)Cmd.login_result_s);
        h.SetBodyLength(1);
        h.Serialize(p);

        p.PushByte((byte)code);

        return p.ToByteArray();
    }
}
