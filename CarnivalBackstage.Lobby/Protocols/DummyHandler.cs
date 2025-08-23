using CarnivalBackstage.Lobby.Binary;

namespace CarnivalBackstage.Lobby.Protocols;

internal class DummyHandler : ProtocolHandler
{
    public override void Handle(Client client, UnPacker unPacker)
    {
        throw new NotSupportedException();
    }
}
