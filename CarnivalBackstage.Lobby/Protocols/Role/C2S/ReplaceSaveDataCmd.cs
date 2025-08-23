using CarnivalBackstage.Lobby.Binary;

namespace CarnivalBackstage.Lobby.Protocols.Role.C2S;

internal class ReplaceSaveDataCmd : IParsableCmd
{
    public byte[]? m_save_data;

    public bool Parse(UnPacker unPacker)
    {
        uint saveDataLenght = 0;
        if (!unPacker.PopUInt32(ref saveDataLenght)) return false;

        m_save_data = new byte[saveDataLenght];
        if (!unPacker.PopByteArray(ref m_save_data)) return false;

        return true;
    }
}
