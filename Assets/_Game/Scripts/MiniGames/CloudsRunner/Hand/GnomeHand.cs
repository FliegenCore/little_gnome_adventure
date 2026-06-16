using _Game.Scripts.FSM;
using _Game.Scripts.UpdateSystems;

namespace _Game.Scripts.MiniGames.CloudsRunner.Hand
{
    public class GnomeHand : IUpdateListener
    {
        private readonly GnomeHandModel _handModel;
        private readonly GnomeHandView _handView;
        
        private readonly Fsm _handFsm;
        
        public GnomeHand(GnomeHandModel gnomeHandModel, GnomeHandView gnomeHandView, Fsm handFsm)
        {
            _handModel =  gnomeHandModel;
            _handView = gnomeHandView;
            _handFsm = handFsm;
        }

        public void Update(float deltaTime)
        {
            _handFsm?.Update(deltaTime);
        }
    }
}