namespace CarnivalBackstage.Lobby.Protocols;

internal interface ISendableCmd
{
    public byte[] Serialize();
}
