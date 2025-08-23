namespace CarnivalBackstage.Lobby.Binary;

internal class CompressionHeader
{
    public const int HEADER_LENGTH = 6;

    public uint m_iLength;

    public byte m_cCompress;

    public byte m_cSecurity;
}
