using CarnivalBackstage.Lobby.Binary;

namespace CarnivalBackstage.Lobby.Protocols.Common;

internal class CommonHandler : ProtocolHandler
{
    public override void Handle(Client client, UnPacker unPacker)
    {
        switch ((Cmd)unPacker.header.m_cCmd)
        {
            case Cmd.heartbeat_cs:
                Console.WriteLine("Sending back");
                _ = client.SendPacket(new HeartbeatCmd().Serialize());
                return;
            default:
                Console.WriteLine($"Cant handle Common Protocol, cmd: {(Cmd)unPacker.header.m_cCmd}, bytes: {unPacker.header.m_iLength}");
                return;
        }
    }
}
