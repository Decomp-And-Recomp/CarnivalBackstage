using CarnivalBackstage.Lobby.Protocols.Role.S2C;
using CarnivalBackstage.Lobby.Protocols.RPG.S2C;

namespace CarnivalBackstage.Lobby.Helpers;

internal static class AccountHelper
{
    public static async Task TryCreateAccount(Client client, string userId)
    {
        // client needs both Role data and RPG data

        await client.SendPacket(new RegisterResultCmd(RegisterResultCmd.Code.kOk).Serialize());

        client.saveData = new();

        await UpdateRpgData(client);

        var data = new NotifyRoleDataCmd();
        data.m_bag_data.m_bag_capacity = 25;
        await client.SendPacket(data.Serialize());
    }

    public static async Task UpdateRpgData(Client client)
    {
        NotifyRPGDataCmd rpgData = new(client);
        await client.SendPacket(rpgData.Serialize());
    }
}
