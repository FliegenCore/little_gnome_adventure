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
                
                if (GUI.Button(new Rect(10, y, 500, 30), door.DoorModel.ConnectedDoorId))
                {
                    door.Interact(null);
                }

                y += 40;
            }
        }
        
        private Vector2 GetScreenRatio()
        {
            int width = Screen.width;
            int height = Screen.height;

            int gcd = FindGCD(width, height);

            return new Vector2(width / gcd, height / gcd);
        }
        
        private int FindGCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
#endif
    }
}