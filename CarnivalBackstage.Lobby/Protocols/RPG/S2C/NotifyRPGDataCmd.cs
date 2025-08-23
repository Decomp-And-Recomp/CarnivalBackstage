using CarnivalBackstage.Lobby.Binary;
using CarnivalBackstage.Lobby.Protocols.Objects;
using System.Text;

namespace CarnivalBackstage.Lobby.Protocols.RPG.S2C;

internal class NotifyRPGDataCmd : ISendableCmd
{
    public uint m_rpg_level;

    public uint m_rpg_lv_exp;

    public uint m_medal;

    public uint m_mobility_time;

    public uint m_coupon;

    public byte m_leader_pos;

    public MemberSlot[] m_member_slot = new MemberSlot[6];

    public uint m_card_capacity;

    public Dictionary<uint, List<ulong>> m_card_list = new Dictionary<uint, List<ulong>>();

    public uint m_jewel_capacity;

    public Dictionary<ushort, uint> m_jewel_list = new Dictionary<ushort, uint>();

    public uint m_equip_capacity;

    public Dictionary<ulong, Equip> m_equip_bag = new Dictionary<ulong, Equip>();

    public uint m_last_refresh_time;

    public MapPoint[] m_mapPoint = new MapPoint[100];

    public NotifyRPGDataCmd()
    {
        for (int i = 0; i < m_member_slot.Length; i++) m_member_slot[i] = new();
        for (byte i = 0; i < m_mapPoint.Length; i++)
        {
            m_mapPoint[i] = new();
            m_mapPoint[i].m_index = i;
        }
    }

    public byte[] Serialize()
    {
        BufferWriter dataWriter = new();

        dataWriter.PushUInt32(m_rpg_level);
        dataWriter.PushUInt32(m_rpg_lv_exp);
        dataWriter.PushUInt32(m_medal);
        dataWriter.PushUInt32(m_mobility_time);
        dataWriter.PushUInt32(m_coupon);
        dataWriter.PushByte(m_leader_pos);

        foreach (MemberSlot slot in m_member_slot)
        {
            dataWriter.PushUInt32(slot.m_member);
            dataWriter.PushUInt64(slot.m_unqiue);
            dataWriter.PushUInt64(slot.m_head);
            dataWriter.PushUInt64(slot.m_body);
            dataWriter.PushUInt64(slot.m_leg);
        }

        dataWriter.PushUInt32(m_card_capacity);

        dataWriter.PushUInt32((uint)m_card_list.Count);

        foreach (var v in m_card_list)
        {
            dataWriter.PushUInt32(v.Key);
            dataWriter.PushUInt32((uint)v.Value.Count);

            foreach (var vv in v.Value)
            {
                dataWriter.PushUInt64(vv);
            }
        }

        dataWriter.PushUInt32(m_jewel_capacity);

        dataWriter.PushUInt32((uint)m_jewel_list.Count);

        foreach (var v in m_jewel_list)
        {
            dataWriter.PushUInt16(v.Key);
            dataWriter.PushUInt32(v.Value);
        }

        dataWriter.PushUInt32(m_equip_capacity);

        dataWriter.PushUInt32((uint)m_equip_bag.Count);

        foreach (var v in m_equip_bag)
        {
            dataWriter.PushUInt64(v.Value.m_id);
            dataWriter.PushUInt64(v.Value.m_part);
            dataWriter.PushUInt64(v.Value.m_state);

            byte[] val9 = new byte[33];
            byte[] md5 = Encoding.UTF8.GetBytes(v.Value.m_md5);
            Array.Copy(md5, val9, md5.Length);

            dataWriter.PushByteArray(val9);
            dataWriter.PushUInt16(v.Value.m_type);
            dataWriter.PushByte(v.Value.m_level);
        }

        dataWriter.PushUInt32(m_last_refresh_time);

        foreach (var v in m_mapPoint)
        {
            dataWriter.PushByte(v.m_status);
            dataWriter.PushUInt32(v.m_role_id);
            dataWriter.PushUInt32(v.m_start_time);
        }

        BufferWriter packetWriter = new();
        Header h = new(7, 0);
        byte[] data = dataWriter.ToByteArray();
        h.SetBodyLength(data.Length);
        h.Serialize(packetWriter);
        packetWriter.PushByteArray(data);

        return packetWriter.ToByteArray();
    }
}