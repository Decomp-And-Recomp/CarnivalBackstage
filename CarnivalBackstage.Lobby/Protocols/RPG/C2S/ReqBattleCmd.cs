using CarnivalBackstage.Lobby.Binary;

namespace CarnivalBackstage.Lobby.Protocols.RPG.C2S;

internal class ReqBattleCmd : IParsableCmd
{
    public byte m_map_point;

    public bool Parse(UnPacker unPacker)
    {
        return unPacker.PopByte(ref m_map_point);
    }
}
