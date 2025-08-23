using CarnivalBackstage.Lobby.Binary;

namespace CarnivalBackstage.Lobby.Protocols.RPG;

internal class RPGHandler : ProtocolHandler
{
    public override void Handle(Client client, UnPacker unPacker)
    {
        switch ((Cmd)unPacker.header.m_cCmd)
        {
            default:
                Console.WriteLine($"Cant handle RPG Protocol, cmd: {(Cmd)unPacker.header.m_cCmd}, bytes: {unPacker.header.m_iLength}");
                return;
        }
    }
}
