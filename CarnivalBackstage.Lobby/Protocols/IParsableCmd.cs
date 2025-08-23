using CarnivalBackstage.Lobby.Binary;

namespace CarnivalBackstage.Lobby.Protocols;

internal interface IParsableCmd
{
    public bool Parse(UnPacker unPacker) => true;
}
