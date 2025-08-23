using CarnivalBackstage.Lobby.Protocols.Role.S2C;
using CarnivalBackstage.Lobby.Protocols.RPG.S2C;

namespace CarnivalBackstage.Lobby.Helpers;

internal static class AccountHelper
{
    public static async Task TryCreateAccount(Client client, string userId)
    {
        // client needs both Role data and RPG data

        await client.SendPacket(new RegisterResultCmd(RegisterResultCmd.Code.kOk).Serialize());

        var rpgData = new NotifyRPGDataCmd();
        rpgData.m_card_capacity = 35;
        await client.SendPacket(rpgData.Serialize());
        await client.SendPacket(new NotifyRoleDataCmd().Serialize());
    }
}
