namespace CarnivalBackstage.Lobby;

internal static class ServerUtils
{
    readonly static Random random = new();

    public static byte[] GenerateSessionKey()
    {
        byte[] b = new byte[87];
        random.NextBytes(b);
        return b;
    }

    public static int GetLength(List<byte> data)
    {
        byte[] bytes = data.Take(8).ToArray();

        //LobbyUtils.Decrypt(ref bytes);

        return WatchInt32(bytes, 0);
    }

    // the packer packs as UInt but i did not want any unnecesary conveting
    static int WatchInt32(byte[] data, int pos)
    {
        return (data[pos] << 24) |
               (data[pos + 1] << 16) |
               (data[pos + 2] << 8) |
               data[pos + 3];
    }
}
