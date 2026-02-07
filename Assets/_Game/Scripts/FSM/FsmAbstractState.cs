namespace _Game.Scripts.FSM
{
    public abstract class FsmAbstractState
    {
        protected readonly Fsm _fsm;
        
        public FsmAbstractState(Fsm fsm)
        {
            _fsm = fsm;
        }
        
        public abstract void Enter();
        public abstract void Exit();
        public abstract void Update(float deltaTime);
    }
}