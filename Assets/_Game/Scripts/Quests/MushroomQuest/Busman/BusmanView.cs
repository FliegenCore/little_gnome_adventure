using _Game.Scripts.PlayerSystems.Animations;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using UnityEngine;

namespace _Game.Scripts.Quests.MushroomQuest.Busman.States
{
    [RequireComponent(typeof(AnimationControl))]
    public class BusmanView : NightstandView
    {
        [field: SerializeField] public AnimationControl AnimationControl { get; private set; }
        [field: SerializeField] public Transform CameraFollowPoint { get; private set; }
        [field: SerializeField] public Transform PlayerJumpPoint { get; private set; }
    }
}