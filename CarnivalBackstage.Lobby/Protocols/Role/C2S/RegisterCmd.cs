using CarnivalBackstage.Lobby.Binary;

namespace CarnivalBackstage.Lobby.Protocols.Role.C2S;

internal class RegisterCmd : IParsableCmd
{
    public string nickname = string.Empty;

    public bool Parse(UnPacker unPacker)
    {
        byte[] data = new byte[unPacker.header.m_iLength - Header.HEADER_LENGTH];

        if (!unPacker.PopByteArray(ref data)) return false;
        nickname = System.Text.Encoding.UTF8.GetString(data);

        return true;
    }
}
