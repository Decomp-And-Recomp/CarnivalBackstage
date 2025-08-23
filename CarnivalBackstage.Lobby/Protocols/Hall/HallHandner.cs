using CarnivalBackstage.Lobby.Binary;
using CarnivalBackstage.Lobby.Protocols.Hall.S2C;

namespace CarnivalBackstage.Lobby.Protocols.Hall;

internal class HallHandler : ProtocolHandler
{
    public override void Handle(Client client, UnPacker unPacker)
    {
        switch ((Cmd)unPacker.header.m_cCmd)
        {
            default:
                Console.WriteLine($"Cant handle Hall Protocol, cmd: {(Cmd)unPacker.header.m_cCmd}, bytes: {unPacker.header.m_iLength}");
                return;
        }
    }
}
