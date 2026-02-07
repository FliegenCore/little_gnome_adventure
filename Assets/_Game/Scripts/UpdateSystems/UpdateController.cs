using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.UpdateSystems
{
    public class UpdateController : MonoBehaviour
    {
        private List<IUpdateListener> _updateListeners = new List<IUpdateListener>();
        
        public void AddListener(IUpdateListener listener)
        {
            if(!_updateListeners.Contains(listener))
                _updateListeners.Add(listener);
        }

        public void RemoveListener(IUpdateListener listener)
        {
            if(_updateListeners.Contains(listener))
                _updateListeners.Remove(listener);
        }

        private void Update()
        {
            for (int i = 0; i < _updateListeners.Count; i++)
            {
                _updateListeners[i].Update(Time.deltaTime);
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.M))
            {
                Application.targetFrameRate = 200;
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.L))
            {
                Application.targetFrameRate = 30;
            }
        }
    }
}