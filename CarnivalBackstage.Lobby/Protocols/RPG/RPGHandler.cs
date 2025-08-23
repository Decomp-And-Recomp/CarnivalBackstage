using CarnivalBackstage.Lobby.Binary;
using CarnivalBackstage.Lobby.Helpers;
using CarnivalBackstage.Lobby.Protocols.RPG.C2S;
using CarnivalBackstage.Lobby.Protocols.RPG.S2C;

namespace CarnivalBackstage.Lobby.Protocols.RPG;

internal class RPGHandler : ProtocolHandler
{
    public override void Handle(Client client, UnPacker unPacker)
    {
        switch ((Cmd)unPacker.header.m_cCmd)
        {
            case Cmd.first_rpg_finish_c:
                OnFirstFinish(client, unPacker);
                return;
            default:
                Console.WriteLine($"Cant handle RPG Protocol, cmd: {(Cmd)unPacker.header.m_cCmd}, bytes: {unPacker.header.m_iLength}");
                return;
        }
    }

    static void OnFirstFinish(Client client, UnPacker unPacker)
    {
        FirstRpgFinishCmd cmd = new();
        if (!cmd.Parse(unPacker)) return;

        foreach (var v in cmd.m_card_list)
            client.saveData.cardList.Add(v, []);

        _ = AccountHelper.UpdateRpgData(client);

        _ = client.SendPacket(new FirstRpgFinishResultCmd().Serialize());
    }
}
