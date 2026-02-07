using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.UpdateSystems
{
    public class FixedUpdateController : MonoBehaviour
    {
        private List<IFixedUpdateListener> _updateListeners = new List<IFixedUpdateListener>();
        
        public void AddListener(IFixedUpdateListener listener)
        {
            if(!_updateListeners.Contains(listener))
                _updateListeners.Add(listener);
        }

        public void RemoveListener(IFixedUpdateListener listener)
        {
            if(_updateListeners.Contains(listener))
                _updateListeners.Remove(listener);
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < _updateListeners.Count; i++)
            {
                _updateListeners[i].FixedUpdate(Time.fixedDeltaTime);
            }
        }
    }
}