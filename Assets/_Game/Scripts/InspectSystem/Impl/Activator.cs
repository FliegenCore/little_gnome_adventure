using System;
using _Game.Scripts.Utils;
using UniRx;
using UnityEngine;

namespace _Game.Scripts.PlayerSystems.InspectSystem
{
    public class Activator : MonoBehaviour
    {
        private ReactiveProperty<bool> IsOpen;
        
        public void Construct(ReactiveProperty<bool> isOpen)
        {
            IsOpen = isOpen;
            
            IsOpen.Subscribe(SetActive).AddTo(gameObject);
        }

        private void SetActive(bool isOpen)
        {
            gameObject.SetActive(isOpen);
        }
    }
}