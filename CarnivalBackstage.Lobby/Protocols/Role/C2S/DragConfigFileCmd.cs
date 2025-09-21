using CarnivalBackstage.Lobby.Binary;
using System.Text;

namespace CarnivalBackstage.Lobby.Protocols.Role.C2S;

internal class DragConfigFileCmd : IParsableCmd
{
    public string filename = string.Empty;

    public bool Parse(UnPacker unPacker)
    {
        byte[] data = new byte[7];
        if (!unPacker.PopByteArray(ref data)) return false;
        filename = Encoding.UTF8.GetString(data);

        return true;
    }
}
