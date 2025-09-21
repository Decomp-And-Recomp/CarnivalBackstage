using CarnivalBackstage.Lobby.Binary;

namespace CarnivalBackstage.Lobby.Protocols.RPG.C2S;

internal class ChangeMemberCmd : IParsableCmd
{
    public byte m_pos;

    public uint m_card_id;

    public ulong m_card_unique_id;

    public bool Parse(UnPacker unPacker)
    {
        if (!unPacker.PopByte(ref m_pos)) return false;
        if (!unPacker.PopUInt32(ref m_card_id)) return false;
        if (!unPacker.PopUInt64(ref m_card_unique_id)) return false;

        return true;
    }
}
