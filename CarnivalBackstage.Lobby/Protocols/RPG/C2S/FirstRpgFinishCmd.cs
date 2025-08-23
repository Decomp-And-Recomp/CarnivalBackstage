using CarnivalBackstage.Lobby.Binary;

namespace CarnivalBackstage.Lobby.Protocols.RPG.C2S;

internal class FirstRpgFinishCmd : IParsableCmd
{
    public readonly List<uint> m_card_list = new();

    public bool Parse(UnPacker unPacker)
    {
        byte cardAmount = 0;
        if (!unPacker.PopByte(ref cardAmount)) return false;

        uint val = 0;
        for (byte b = 0; b < cardAmount; b++)
        {
            if (!unPacker.PopUInt32(ref val)) return false;
            m_card_list.Add(val);
        }

        return true;
    }
}
