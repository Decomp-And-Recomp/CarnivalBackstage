using CarnivalBackstage.Lobby.Binary;
using CarnivalBackstage.Lobby.Protocols.Role.C2S;
using CarnivalBackstage.Lobby.Protocols.Role.S2C;

namespace CarnivalBackstage.Lobby.Protocols.Role;

internal class RoleHandler : ProtocolHandler
{
    public override void Handle(Client client, UnPacker unPacker)
    {
        switch ((Cmd)unPacker.header.m_cCmd)
        {
            case Cmd.login_c:
                OnLoginRequest(client);
                return;
            case Cmd.drag_config_list_c:
                OnDragConfigListRequest(client);
                return;
            case Cmd.register_c:
                OnRegisterRequest(client, unPacker);
                return;
            case Cmd.sync_server_time_c:
                OnSyncServerTimeRequest(client);
                return;
            case Cmd.get_all_model_num_c:
                _ = client.SendPacket(new GetAllModelNumResultCmd().Serialize());
                return;
            case Cmd.get_forbid_friend_request_c:
                _ = client.SendPacket(new GetForbidFriendRequestResult(0).Serialize());
                return;
            default:
                Console.WriteLine($"Cant handle Role Protocol, cmd: {(Cmd)unPacker.header.m_cCmd}, bytes: {unPacker.header.m_iLength}");
                return;
        }
    }

    void OnLoginRequest(Client client)
    {
        _ = client.SendPacket(new LoginResultCmd(LoginResultCmd.Code.kNeedRegister).Serialize());
        //_ = client.SendPacket(new LoginResultCmd(LoginResultCmd.Code.kOk).Serialize());
    }

    void OnDragConfigListRequest(Client client)
    {
        _ = client.SendPacket(new DragConfigListResultCmd().Serialize());
    }

    void OnRegisterRequest(Client client, UnPacker unPacker)
    {
        RegisterCmd cmd = new();
        if (!cmd.Parse(unPacker))
        {
            _ = client.SendPacket(new RegisterResultCmd(RegisterResultCmd.Code.kError).Serialize());
            return;
        }

        _ = client.SendPacket(new RegisterResultCmd(RegisterResultCmd.Code.kOk).Serialize());
        //_ = client.SendPacket(new UploadRoleDataResultCmd(UploadRoleDataResultCmd.Code.kOk).Serialize());
        _ = client.SendPacket(new NotifyRoleDataCmd().Serialize());
    }

    void OnSyncServerTimeRequest(Client client)
    {
        _ = client.SendPacket(new SyncServerTimeResultCmd((uint)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()).Serialize());
    }
}
