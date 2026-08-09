using System;
using _Game.Scripts.InteractionSystems;
using UniRx;
using UnityEngine;

namespace _Game.Scripts.Quests.ClanDoorQuest.Gates
{
    public class HellGatesModel : AbstractInteractableModel
    {
        public readonly Subject<Action> OpenDoor = new Subject<Action>();
        public bool CanInteract = true;
        
        public HellGatesModel(IContactTriggerProvider contactTriggerProvider,
            Vector2 position,
            string id
            ) : base(contactTriggerProvider, position, id)
        {
            CanInteract = true;
        }
    }
}