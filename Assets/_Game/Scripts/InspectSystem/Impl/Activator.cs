using System;
using _Game.Scripts.Utils;
using UnityEngine;

namespace _Game.Scripts.PlayerSystems.InspectSystem
{
    public class Activator : MonoBehaviour
    {
        private Observable<bool> IsOpen;
        
        public void Construct(Observable<bool> isOpen)
        {
            IsOpen = isOpen;
            
            IsOpen.Subscribe(SetActive);
        }

        private void SetActive(bool isOpen)
        {
            gameObject.SetActive(isOpen);
        }

        private void OnDestroy()
        {
            IsOpen.Unsubscribe(SetActive);
        }
    }
}