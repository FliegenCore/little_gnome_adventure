using _Game.Scripts.Quests.ClanDoorQuest.Gates.View;
using UnityEngine;

namespace _Game.Scripts.RoomSystems.Impl.FuckingHellWithGates
{
    public class FuckingHellWithGatesLocationView : AbstractLocationView
    {
        [field: SerializeField] public HellGatesView HellGatesView { get; private set; }
    }
}