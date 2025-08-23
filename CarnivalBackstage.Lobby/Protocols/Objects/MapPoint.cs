namespace CarnivalBackstage.Lobby.Protocols.Objects;

internal class MapPoint
{
    public enum EState
    {
        kLock = 0,
        kNpc = 1,
        KGold = 2,
        kPlayer = 3
    }

    public byte m_index = 0;

    public byte m_status = 0;

    public uint m_role_id = 0;

    public uint m_start_time = 0;
}
