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
            case Cmd.req_battle_c:
                OnRequestBattle(client, unPacker);
                return;
            case Cmd.change_member_c:
                OnChangeMember(client, unPacker);
                return;
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
        {
            if (!client.saveData.cardList.ContainsKey(v)) client.saveData.cardList[v] = [];

            client.saveData.cardList[v].Add((ulong)Random.Shared.Next());
        }

        _ = client.SendPacket(new NotifyRPGDataCmd(client).Serialize()); // YES YOU ARE MEANT TO UPDATE DATA MANUALY
        _ = client.SendPacket(new FirstRpgFinishResultCmd().Serialize());
    }

    static void OnChangeMember(Client client, UnPacker unPacker)
    {
        ChangeMemberCmd cmd = new();
        if (!cmd.Parse(unPacker)) return;

        // 1: change data here (in client)
        // 2: send result (half done)
        ChangeMemberResultCmd result = new();
        result.m_pos = cmd.m_pos;
        result.m_result = ChangeMemberResultCmd.Code.kOk;
        result.m_member_card = cmd.m_card_id;
        result.m_member_unique_id = cmd.m_card_unique_id;
        result.m_unmember_card = 0;
        result.m_unmember_unique_id = 0;
        
        _ = client.SendPacket(result.Serialize());
    }

    static void OnRequestBattle(Client client, UnPacker unPacker)
    {
        ReqBattleCmd cmd = new();
        if (!cmd.Parse(unPacker)) return;

        _ = client.SendPacket(new ReqBattleResultCmd(ReqBattleResultCmd.Code.kOk).Serialize());
    }
}
