using System.Net.Sockets;

namespace CarnivalBackstage.Lobby;

internal class Client
{
    readonly Server server;

    public readonly TcpClient tcpClient;
    public readonly NetworkStream stream;

    public readonly byte[] encryptionKey;

    public ClientSaveData saveData = null!;

    public Client(Server server, TcpClient tcpClient, byte[] encryptionKey)
    {
        this.server = server;

        this.tcpClient = tcpClient;
        this.encryptionKey = encryptionKey;

        stream = tcpClient.GetStream();
    }

    public async Task SendPacket(byte[] data, bool encrypt = true)
    {
        await server.SendPacketToClient(this, data, encrypt);
    }
}
