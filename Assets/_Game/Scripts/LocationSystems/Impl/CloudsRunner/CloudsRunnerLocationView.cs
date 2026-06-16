using System;
using _Game.Scripts.MiniGames.CloudsRunner.Hand;
using UnityEngine;

namespace _Game.Scripts.RoomSystems.Impl.CloudsRunner
{
    public class CloudsRunnerLocationView : AbstractLocationView
    {
        [field: SerializeField] public GnomeHandView GnomeHandView { get; private set; }
    }
}