using CarnivalBackstage.Lobby.Binary;

namespace CarnivalBackstage.Lobby.Protocols.RPG.S2C;

internal class ReqBattleResultCmd : ISendableCmd
{
    public enum Code : byte
    {
        kOk = 0,
        kError = 1
    }

    readonly Code code;

    public ReqBattleResultCmd(Code code)
    {
        this.code = code;
    }

    public byte[] Serialize()
    {
        BufferWriter w = new();
        Header h = new(Cmd.req_battle_result_s);
        h.SetBodyLength(1);
        h.Serialize(w);

        w.PushByte((byte)code);

        return w.ToByteArray();
    }
}
