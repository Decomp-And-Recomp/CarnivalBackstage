namespace CarnivalBackstage.Lobby.Binary;

internal class Header
{
    public const uint HEADER_LENGTH = 10u;

    public uint m_iLength;

    public byte m_cProtocol;

    public byte m_cCmd;

    public uint m_iReserve;

    public Header() { }

    public Header(byte protocol, byte cmd)
    {
        m_cProtocol = protocol;
        m_cCmd = cmd;
    }

    public Header(Protocols.Common.Cmd cmd)
    {
        m_cProtocol = 0;
        m_cCmd = (byte)cmd;
    }

    public Header(Protocols.Role.Cmd cmd)
    {
        m_cProtocol = 1;
        m_cCmd = (byte)cmd;
    }
    
    public Header(Protocols.Shop.Cmd cmd)
    {
        m_cProtocol = 2;
        m_cCmd = (byte)cmd;
    }

    public Header(Protocols.Hall.Cmd cmd)
    {
        m_cProtocol = 3;
        m_cCmd = (byte)cmd;
    }

    public Header(Protocols.Mail.Cmd cmd)
    {
        m_cProtocol = 4;
        m_cCmd = (byte)cmd;
    }

    public Header(Protocols.Rank.Cmd cmd)
    {
        m_cProtocol = 5;
        m_cCmd = (byte)cmd;
    }

    public Header(Protocols.RPG.Cmd cmd)
    {
        m_cProtocol = 7;
        m_cCmd = (byte)cmd;
    }

    public virtual void SetBodyLength(int body_length)
    {
        m_iLength = (uint)(10 + body_length);
    }

    public void Serialize(BufferWriter writer)
    {
        writer.PushUInt32(m_iLength);
        writer.PushByte(m_cProtocol);
        writer.PushByte(m_cCmd);
        writer.PushUInt32(m_iReserve);
    }

    public bool Parse(BufferReader reader)
    {
        if (!reader.PopUInt32(ref m_iLength)) return false;
        if (!reader.PopByte(ref m_cProtocol)) return false;
        if (!reader.PopByte(ref m_cCmd)) return false;
        if (!reader.PopUInt32(ref m_iReserve)) return false;

        return true;
    }
}
