using _Game.Scripts.Characters.Rabbit.Animations;
using _Game.Scripts.FSM;
using _Game.Scripts.PlayerSystems.InspectSystem;
using _Game.Scripts.Quests.StartGameQuest.Rabbit.States;
using _Game.Scripts.UpdateSystems;
using Game.PlayerSystem;

namespace _Game.Scripts.Quests.StartGameQuest.Rabbit
{
    public class RabbitFactory
    {
        private Rabbit _cachedRabbit;
        
        public Rabbit CachedRabbit => _cachedRabbit;

        private readonly UpdateController _updateController;
        
        public RabbitFactory(UpdateController updateController)
        {
            _updateController = updateController;
        }
        
        public Rabbit CreateRabbit(RabbitView rabbitView)
        {
            Transformation transformation =
                new Transformation(rabbitView.transform.position, rabbitView.transform.localScale);

            RabbitAnimationModel rabbitAnimationModel = new RabbitAnimationModel();
            
            rabbitView.RabbitAnimationView.Construct(rabbitAnimationModel);
            rabbitView.Transformable.Construct(transformation);
            
            RabbitModel rabbitModel = new RabbitModel(
                transformation,
                rabbitAnimationModel
                );
            
            rabbitView.Activator.Construct(rabbitModel.IsActive);
            Fsm fsm = CreateFsm(rabbitModel);
            
            Rabbit rabbit = new Rabbit(fsm, rabbitModel);

            _cachedRabbit = rabbit;
            _updateController.AddListener(_cachedRabbit);
            return rabbit;
        }

        private Fsm CreateFsm(RabbitModel rabbitModel)
        {
            Fsm fsm = new Fsm();

            fsm.AddState(new RabbitSeatState(fsm, rabbitModel));
            fsm.AddState(new RabbitJumpState(fsm, rabbitModel));
            fsm.AddState(new RabbitIdleState(fsm, rabbitModel));
            fsm.AddState(new RabbitAutoWalkState(fsm, rabbitModel));
            fsm.AddState(new RabbitWaitCatchState(fsm, rabbitModel));
            
            return fsm;
        }
    }
}