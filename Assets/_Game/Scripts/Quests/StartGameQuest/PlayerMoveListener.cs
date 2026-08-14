using System;
using _Game.Scripts.PlayerSystems;
using VContainer.Unity;

namespace _Game.Scripts.Quests.StartGameQuest
{
    public class PlayerMoveListener : IInitializable
    {
        public event Action OnPlayerMoved;

        private readonly IPlayerFactory _playerFactory;
        private Player _player;

        private PlayerMoveListener(IPlayerFactory playerFactory)
        {
            _playerFactory = playerFactory;
        }
        
        public void Initialize()
        {
            _playerFactory.Subscribe(FillPlayer);
        }

        private void FillPlayer(Player player)
        {
            _player = player;

            _player.PlayerModel.MoveDirectionInput.OnStartPlayerMoved += InvokeOnPlayerMoved;
        }

        private void InvokeOnPlayerMoved()
        {
            OnPlayerMoved?.Invoke();
        }
    }
}