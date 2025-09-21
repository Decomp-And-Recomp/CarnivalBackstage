using CarnivalBackstage.Lobby.Binary;

namespace CarnivalBackstage.Lobby.Protocols.Role.S2C;

internal class GetAllModelNumResultCmd : ISendableCmd
{
    public byte[] Serialize()
    {
        BufferWriter p = new();

        Header h = new((byte)ProtoID.ROLE, (byte)Cmd.get_all_model_num_result_s);
        h.SetBodyLength(1);
        h.Serialize(p);

        p.PushByte(0);

        return p.ToByteArray();
    }
}
