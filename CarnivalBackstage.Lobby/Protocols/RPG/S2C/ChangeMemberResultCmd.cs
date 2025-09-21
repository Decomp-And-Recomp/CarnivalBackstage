using CarnivalBackstage.Lobby.Binary;

namespace CarnivalBackstage.Lobby.Protocols.RPG.S2C;

internal class ChangeMemberResultCmd : ISendableCmd
{
    public enum Code : byte
    {
        kOk = 0,
        kDataError = 1
    }

    public Code m_result;

    public byte m_pos;

    public uint m_member_card;

    public ulong m_member_unique_id;

    public uint m_unmember_card;

    public ulong m_unmember_unique_id;

    public byte[] Serialize()
    {
        BufferWriter w = new();
        Header h = new(Cmd.change_member_result_s);
        h.SetBodyLength(14);
        h.Serialize(w);

        w.PushByte((byte)m_result);
        w.PushByte(m_pos);
        w.PushUInt32(m_member_card);
        w.PushUInt64(m_member_unique_id);
        w.PushUInt32(m_unmember_card);
        w.PushUInt64(m_unmember_unique_id);

        return w.ToByteArray();
    }
}
