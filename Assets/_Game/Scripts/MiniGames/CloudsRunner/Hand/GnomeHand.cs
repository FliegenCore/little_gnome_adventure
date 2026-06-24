using _Game.Scripts.FSM;
using _Game.Scripts.MiniGames.CloudsRunner.Hand.States;
using _Game.Scripts.UpdateSystems;

namespace _Game.Scripts.MiniGames.CloudsRunner.Hand
{
    public class GnomeHand : IUpdateListener
    {
        public readonly GnomeHandModel HandModel;
        private readonly GnomeHandView _handView;
        
        private readonly Fsm _handFsm;
        
        public GnomeHand(GnomeHandModel gnomeHandModel, GnomeHandView gnomeHandView, Fsm handFsm)
        {
            HandModel =  gnomeHandModel;
            _handView = gnomeHandView;
            _handFsm = handFsm;
        }

        public void Update(float deltaTime)
        {
            _handFsm?.Update(deltaTime);
        }

        public void SetState<T>() where T : GnomeHandState
        {
            _handFsm.SetState<T>();
        }
    }
}