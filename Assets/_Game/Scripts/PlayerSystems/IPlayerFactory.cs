namespace _Game.Scripts.PlayerSystems
{
    public interface IPlayerFactory
    {
        Player CreatePlayer();
        Player GetPlayer();
    }
}