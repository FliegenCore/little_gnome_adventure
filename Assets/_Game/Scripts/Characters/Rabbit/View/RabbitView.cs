using _Game.Scripts.Characters.Rabbit.Animations;
using _Game.Scripts.PlayerSystems.InspectSystem;
using Game.PlayerSystem;
using UnityEngine;

namespace _Game.Scripts.Quests.StartGameQuest.Rabbit
{
    public class RabbitView : MonoBehaviour
    {
        [field: SerializeField] public Transformable Transformable { get; private set; }
        [field: SerializeField] public Activator Activator { get; private set; }
        [field: SerializeField] public RabbitAnimationView RabbitAnimationView { get; private set; }
    }
}