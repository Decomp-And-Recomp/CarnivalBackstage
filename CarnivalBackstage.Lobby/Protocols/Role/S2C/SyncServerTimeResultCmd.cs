using CarnivalBackstage.Lobby.Binary;

namespace CarnivalBackstage.Lobby.Protocols.Role.S2C;

internal class SyncServerTimeResultCmd : ISendableCmd
{
    //public uint m_local_time;

    public uint m_server_time;

    public SyncServerTimeResultCmd(uint time)
    {
        m_server_time = time;
    }

    public byte[] Serialize()
    {
        BufferWriter p = new();

        Header h = new(1, 71);
        h.SetBodyLength(8);
        h.Serialize(p);

        p.PushUInt32(0); //m_local_time, but its unused on client, still needed for parsing
        p.PushUInt32(m_server_time);

        return p.ToByteArray();
    }
}
