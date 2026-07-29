using _Game.Scripts.PlayerSystems.Animations;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using UnityEngine;

namespace _Game.Scripts.Quests.ClanDoorQuest.Gates.View
{
    public class HellGatesView : NightstandView
    {
        [field: SerializeField] public AnimationControl AnimationControl { get; private set; }
    }
}