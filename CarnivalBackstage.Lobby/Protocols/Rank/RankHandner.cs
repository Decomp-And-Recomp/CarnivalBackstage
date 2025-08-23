using CarnivalBackstage.Lobby.Binary;

namespace CarnivalBackstage.Lobby.Protocols.Rank;

internal class RankHandler : ProtocolHandler
{
    public override void Handle(Client client, UnPacker unPacker)
    {
        switch ((Cmd)unPacker.header.m_cCmd)
        {
            default:
                Console.WriteLine($"Cant handle Rank Protocol, cmd: {(Cmd)unPacker.header.m_cCmd}, bytes: {unPacker.header.m_iLength}");
                return;
        }
    }
}
