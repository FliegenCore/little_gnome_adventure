using System;
using _Game.Scripts.PlayerSystems.Animations;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using UniRx;
using UnityEngine;

namespace _Game.Scripts.Quests.ClanDoorQuest.Gates.View
{
    public class HellGatesView : NightstandView
    {
        [field: SerializeField] public AnimationControl AnimationControl { get; private set; }
        [field: SerializeField] public Collider2D GatesCollider { get; private set; }
        
        private Subject<Action> _openDoor;
        
        public void Construct(Subject<Action> openDoor)
        {
            _openDoor = openDoor;
            
            _openDoor
                .Subscribe(PlayOpenDoorAnimation)
                .AddTo(gameObject);
        }

        private void PlayOpenDoorAnimation(Action callback)
        {
            AnimationControl.SetAnimation(0, "open", false, () =>
            {
                GatesCollider.gameObject.SetActive(false);
                callback?.Invoke();
                AnimationControl.SetAnimation(0, "idleopen", false);
            });
        }
    }
}