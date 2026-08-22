using _Game.Scripts.FSM;
using _Game.Scripts.UpdateSystems;
using UnityEngine;

namespace _Game.Scripts.Quests.StartGameQuest.Rabbit
{
    public class Rabbit : IUpdateListener
    {
        public readonly Fsm StateMachine;
        public readonly RabbitModel RabbitModel;
        
        public Rabbit(Fsm stateMachine, RabbitModel rabbitModel)
        {
            RabbitModel  = rabbitModel;
            StateMachine = stateMachine;
        }

        public void Update(float deltaTime)
        {
            StateMachine?.Update(deltaTime);
        }
    }
}