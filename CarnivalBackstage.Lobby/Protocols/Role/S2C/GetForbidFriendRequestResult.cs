using CarnivalBackstage.Lobby.Binary;

namespace CarnivalBackstage.Lobby.Protocols.Role.S2C;

internal class GetForbidFriendRequestResult : ISendableCmd
{
    public byte m_val;

    public GetForbidFriendRequestResult(byte m_val)
    {
        this.m_val = m_val;
    }

    public byte[] Serialize()
    {
        BufferWriter p = new();

        Header h = new((byte)ProtoID.ROLE, (byte)Cmd.get_forbid_friend_request_result_s);
        h.SetBodyLength(1);
        h.Serialize(p);

        p.PushByte(m_val);

        return p.ToByteArray();
    }
}
