using CarnivalBackstage.Lobby.Binary;
using CarnivalBackstage.Lobby.Protocols.Objects;
using System.Text;

namespace CarnivalBackstage.Lobby.Protocols.Role.S2C;

internal class NotifyRoleDataCmd : ISendableCmd
{
    public RoleInfo m_info = new();

    public BagData m_bag_data = new();

    public byte m_hasInportFB;

    public List<uint> m_friend_list = [];

    public List<uint> m_collect_list = [];

    public List<uint> m_follow_list = [];

    public byte[]? m_save_data;

    public byte[] Serialize()
    {
        BufferWriter dataWriter = new();


        dataWriter.PushUInt32(m_info.m_player_id);

        byte[] facebookId = new byte[33];
        dataWriter.PushByteArray(facebookId);

        byte[] faceImageMd5 = new byte[33];
        dataWriter.PushByteArray(facebookId);

        byte[] name = new byte[33];
        //Array.Copy(Encoding.UTF8.GetBytes(m_info.m_name), name, 4);
        Array.Copy(Encoding.UTF8.GetBytes("test"), name, 4);
        dataWriter.PushByteArray(name);

        dataWriter.PushUInt32(m_info.m_level);
        dataWriter.PushUInt64(m_info.m_head);
        dataWriter.PushUInt64(m_info.m_body);
        dataWriter.PushUInt64(m_info.m_leg);
        dataWriter.PushUInt64(m_info.m_head_top);
        dataWriter.PushUInt64(m_info.m_head_front);
        dataWriter.PushUInt64(m_info.m_head_back);
        dataWriter.PushUInt64(m_info.m_head_left);
        dataWriter.PushUInt64(m_info.m_head_right);
        dataWriter.PushUInt64(m_info.m_chest_front);
        dataWriter.PushUInt64(m_info.m_chest_back);
        dataWriter.PushUInt32(m_info.m_heart);
        dataWriter.PushUInt32(m_info.m_gold);
        dataWriter.PushUInt32(m_info.m_crystal);
        dataWriter.PushByte(m_bag_data.m_first_buy_head);
        dataWriter.PushByte(m_bag_data.m_first_buy_body);
        dataWriter.PushByte(m_bag_data.m_first_buy_leg);
        dataWriter.PushByte(m_bag_data.m_bag_capacity);

        dataWriter.PushByte((byte)m_bag_data.m_bag_list.Count);
        foreach (var v in m_bag_data.m_bag_list)
        {
            // serialize
        }

        dataWriter.PushByte(m_hasInportFB);

        dataWriter.PushByte((byte)m_friend_list.Count);
        foreach (var v in m_friend_list) dataWriter.PushUInt32(v);

        dataWriter.PushByte((byte)m_collect_list.Count);
        foreach (var v in m_collect_list) dataWriter.PushUInt32(v);

        dataWriter.PushByte((byte)m_follow_list.Count);
        foreach (var v in m_follow_list) dataWriter.PushUInt32(v);

        if (m_save_data == null) m_save_data = [];

        dataWriter.PushUInt32((uint)m_save_data.Length);
        dataWriter.PushByteArray(m_save_data);

        BufferWriter packetWriter = new();
        Header h = new(1, 15);
        byte[] data = dataWriter.ToByteArray();
        h.SetBodyLength(data.Length);
        h.Serialize(packetWriter);
        packetWriter.PushByteArray(data);

        return packetWriter.ToByteArray();
    }
}
