using CarnivalBackstage.Shared;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace CarnivalBackstage.Lobby.Binary;

internal class Packer : BufferWriter
{
    private List<byte> m_data = new List<byte>();

    public Packer()
    {
        SetData(m_data);
    }

    public Packet MakePacket(byte security, byte compress, byte[]? key)
    {
        byte[] array = m_data.ToArray();
        if (compress == 1)
        {
            MemoryStream memoryStream = new MemoryStream();
            DeflaterOutputStream deflaterOutputStream = new DeflaterOutputStream(memoryStream);
            deflaterOutputStream.Write(array, 0, array.Length);
            deflaterOutputStream.Close();
            array = memoryStream.ToArray();
        }
        if (security == 1)
        {
            if (key == null) throw new NullReferenceException(nameof(key));

            array = XXTEAUtils.Encrypt(array, key);
        }

        int num = CompressionHeader.HEADER_LENGTH + array.Length;

        CompressionHeader header = new()
        {
            m_iLength = (uint)num,
            m_cCompress = compress,
            m_cSecurity = security,
        };

        Packet packet = new(num);
        packet.PushUInt32(header.m_iLength);
        packet.PushByte(header.m_cCompress);
        packet.PushByte(header.m_cSecurity);
        packet.PushByteArray(array, array.Length);

        return packet;
    }
}
