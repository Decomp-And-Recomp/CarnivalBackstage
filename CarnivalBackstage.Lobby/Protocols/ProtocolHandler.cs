using CarnivalBackstage.Lobby.Binary;

namespace CarnivalBackstage.Lobby.Protocols;

internal abstract class ProtocolHandler
{
    public abstract void Handle(Client client, UnPacker unPacker);
}
