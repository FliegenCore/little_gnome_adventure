using _Game.Scripts.InteractionSystems;
using _Game.Scripts.InteractionSystems.Interactables.Items;
using _Game.Scripts.PlayerSystems.Animations.Impl;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using _Game.Scripts.Quests.MushroomQuest.Busman.States;
using _Game.Scripts.Quests.StartGameQuest;
using _Game.Scripts.Quests.StartGameQuest.Rabbit;
using UnityEngine;

namespace _Game.Scripts.RoomSystems.Impl.DreamForest
{
    public class DreamForestLocationView : AbstractLocationView
    {
        [field: SerializeField] public NightstandView McMushroomView { get; private set; }
        [field: SerializeField] public CharacterView OrcWomanView { get; private set; }
        [field: SerializeField] public BaseItemView[] Mushrooms { get; private set; }
        [field: SerializeField] public BusmanView BusmanView { get; private set; }
        [field: SerializeField] public Transform StartMovePoint { get; private set; }
        [field: SerializeField] public Transform RabbitMovePoint { get; private set; }
        [field: SerializeField] public RabbitView RabbitView { get; private set; }
        [field: SerializeField] public MovePointTransform RabbitMovePointTransform { get; private set; }
        [field: SerializeField] public ContactTriggerProvider[] RabbitSetMoveTriggers { get; private set; }
        [field: SerializeField] public ContactTriggerProvider[] PlayerSetSneakTriggers { get; private set; }
    }
}