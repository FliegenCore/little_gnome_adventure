using _Game.Scripts.PlayerSystems;

namespace _Game.Scripts.CameraSystem
{
    public class CameraControllerHelper
    {
        private readonly CameraController _cameraController;
        private readonly IPlayerFactory _playerFactory;
        
        private CameraControllerHelper(CameraController cameraController, IPlayerFactory playerFactory)
        {
            _cameraController = cameraController;
            _playerFactory    = playerFactory;
        }

        public void SetFollowPlayer()
        {
            _cameraController.SetFollowTarget(_playerFactory.GetPlayer().PlayerView.transform);
        }
    }
}