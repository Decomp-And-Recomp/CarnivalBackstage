using CarnivalBackstage.Lobby.Binary;
using System.Text;

namespace CarnivalBackstage.Lobby.Protocols.Role.S2C;

internal class DragConfigFileResultCmd : ISendableCmd
{
    public string? filename;
    public byte[]? data;

    public byte[] Serialize()
    {
        BufferWriter w = new();
        Header h = new((byte)ProtoID.ROLE, (byte)Cmd.drag_config_file_result_s);
        h.SetBodyLength(0);
        h.Serialize(w);

        if (this.filename == null) throw new NullReferenceException(nameof(this.filename));
        if (this.data == null) throw new NullReferenceException(nameof(this.data));

        byte[] filenameRaw = Encoding.UTF8.GetBytes(this.filename);
        byte[] filename = new byte[33];
        Array.Copy(filenameRaw, filename, Math.Min(filenameRaw.Length, 32)); // 32 NOT 33

        w.PushByteArray(filename);

        w.PushUInt32((uint)data.Length);
        w.PushByteArray(data);

        return w.ToByteArray();
    }
}
