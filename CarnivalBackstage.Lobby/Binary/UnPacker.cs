using CarnivalBackstage.Shared;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace CarnivalBackstage.Lobby.Binary;

internal class UnPacker : BufferReader
{
    public Header header = new();

    public bool ParserPacket(Packet packet, byte[] key)
    {
        SetData(packet.ByteArray());
        CompressionHeader compressionHeader = new();

        if (!PopUInt32(ref compressionHeader.m_iLength)) return false;
        if (!PopByte(ref compressionHeader.m_cCompress)) return false;
        if (!PopByte(ref compressionHeader.m_cSecurity)) return false;

        byte[] val = new byte[m_data.Length - m_offset];

        PopByteArray(ref val, val.Length);
        SetData(val);

        if (compressionHeader.m_cSecurity == 1) // Security.kXXTEA
        {
            val = XXTEAUtils.Decrypt(val, key);
            SetData(val);
        }

        if (compressionHeader.m_cCompress == 1) //Compress.kGzipNoHeader
        {
            InflaterInputStream inflaterInputStream = new InflaterInputStream(new MemoryStream(val, 0, val.Length));
            MemoryStream memoryStream = new MemoryStream();
            int num = 0;
            byte[] array = new byte[4096];
            while ((num = inflaterInputStream.Read(array, 0, array.Length)) != 0)
            {
                memoryStream.Write(array, 0, num);
            }
            inflaterInputStream.Close();
            SetData(memoryStream.ToArray());
        }

        return header.Parse(this);
    }
}
