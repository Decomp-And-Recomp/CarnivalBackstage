using CarnivalBackstage.Lobby.Binary;

namespace CarnivalBackstage.Lobby.Protocols.Role.S2C;

internal class ReplaceSaveDataResultCmd : ISendableCmd
{
    public enum Code : byte
    {
        kOk = 0
    }

    readonly Code code;

    public ReplaceSaveDataResultCmd(Code code)
    {
        this.code = code;
    }

    public byte[] Serialize()
    {
        BufferWriter p = new();

        Header h = new((byte)ProtoID.ROLE, (byte)Cmd.replace_save_data_result_s);
        h.SetBodyLength(1);
        h.Serialize(p);

        p.PushByte((byte)code);

        return p.ToByteArray();
    }
}
