using System.Collections.Generic;
using _Game.Scripts.RoomSystems;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.Hacks
{
    public class HackController : MonoBehaviour
    {
        private DoorsService _doorsService;
        
        private bool _isOpen;

        [Inject]
        private void Construct(DoorsService doorsService)
        {
            _doorsService = doorsService;
        }
        
#if UNITY_EDITOR
        public void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.F1))
            {
                _isOpen = !_isOpen; 
            }
        }

        private void OnGUI()
        {
            if (!_isOpen) return;

            int y = 10;

            List<string> drowedNames = new List<string>();
            
            foreach (var door in _doorsService.GetDoors())
            {
                if(drowedNames.Contains(door.DoorModel.ConnectedDoorId))
                    continue;
                
                drowedNames.Add(door.DoorModel.ConnectedDoorId);
                
                if (GUI.Button(new Rect(10, y, 200, 30), door.DoorModel.ConnectedDoorId))
                {
                    door.Interact();
                }

                y += 40;
            }
        }

        private void PerformDebugFunction()
        {
            if (_doorsService != null)
            {
                // Пример использования _doorsService
                Debug.Log("Doors service is available");
            }
        }
#endif
    }
}