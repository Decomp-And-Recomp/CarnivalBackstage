using CarnivalBackstage.Lobby.Binary;
using CarnivalBackstage.Lobby.Protocols;
using CarnivalBackstage.Lobby.Protocols.Common;
using CarnivalBackstage.Lobby.Protocols.Hall;
using CarnivalBackstage.Lobby.Protocols.Mail;
using CarnivalBackstage.Lobby.Protocols.Rank;
using CarnivalBackstage.Lobby.Protocols.Role;
using CarnivalBackstage.Lobby.Protocols.RPG;
using CarnivalBackstage.Lobby.Protocols.Shop;
using System.Net;
using System.Net.Sockets;

namespace CarnivalBackstage.Lobby;

public enum ServerState
{
    NotRunning, Running
}

public class Server
{
    const ushort maxDataLength = 32768;

    readonly TcpListener listener;

    public ServerState state { get; private set; }

    readonly int port;

    readonly ProtocolHandler[] handlers = {
        new CommonHandler(),
        new RoleHandler(),
        new ShopHandler(),
        new HallHandler(),
        new MailHandler(),
        new RankHandler(),
        new DummyHandler(),
        new RPGHandler()
    };

    public Server(int port)
    {
        this.port = port;
        listener = new(IPAddress.Any, port);

        GameConfig.Init();
    }

    public async Task Run()
    {
        try
        {
            listener.Start();
            state = ServerState.Running;

            Console.WriteLine("Lobby now running on port: " + port);
            await ServerLoop();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    async Task ServerLoop()
    {
        while (state == ServerState.Running)
        {
            try
            {
                _ = HandleConnection(await listener.AcceptTcpClientAsync());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }

    async Task HandleConnection(TcpClient tcpClient)
    {
        Client client = new(this, tcpClient, ServerUtils.GenerateSessionKey());

        var stream = tcpClient.GetStream();

        _ = SendPacketToClient(client, new Protocols.Common.S2C.SessionKeyCmd(client.encryptionKey).Serialize(), false);

        List<byte> received = [];
        int read;

        byte[] buffer = new byte[maxDataLength];

        bool lengthCalculated = false;
        int length = 0;

        while (true)
        {
            read = await stream.ReadAsync(buffer);

            if (read == 0)
            {
                received.Clear();
                break;
            }

            received.AddRange(buffer.Take(read));
            //File.WriteAllBytes("test", buffer.Take(read).ToArray());

            while (received.Count > Header.HEADER_LENGTH)
            {
                if (!lengthCalculated)
                {
                    lengthCalculated = true;
                    length = ServerUtils.GetLength(received);

                    if (length > maxDataLength)
                    {
                        Console.WriteLine("[Too Much Data] Amount: " + length);
                        break;
                    }
                    if (length < Header.HEADER_LENGTH)
                    {
                        Console.WriteLine("[Not Enough Data] Amount: " + length);
                        break;
                    }
                }

                if (received.Count < length) break;

                var bytes = received.GetRange(0, length).ToArray();
                received.RemoveRange(0, length);

                OnReceive(bytes, client);

                lengthCalculated = false;
            }
        }
    }

    void OnReceive(byte[] bytes, Client client)
    {
        try
        {
            Packet p = new(bytes, false);

            UnPacker unPacker = new();
            if (!unPacker.ParserPacket(p, client.encryptionKey))
            {
                Console.WriteLine("Couldnt parse a packet.");
                return;
            }

            handlers[unPacker.header.m_cProtocol].Handle(client, unPacker);
        }
        catch (Exception e) 
        {
            Console.WriteLine(e);
        }
    }

    internal async Task SendPacketToClient(Client client, byte[] data, bool encrypt = true)
    {
        Packer packer = new();
        packer.PushByteArray(data);

        //Packet packet = packer.MakePacket(encrypt ? (byte)1 : (byte)0, 0, client.encryptionKey);
        Packet packet = packer.MakePacket(0, 0, client.encryptionKey);

        await client.stream.WriteAsync(packet.ByteArray());
    }
}
