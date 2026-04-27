using System;
using _Game.Scripts.PlayerSystems.Animations;
using Core.Common;
using UnityEngine;

namespace _Game.Scripts.DialogueSystem.View
{
    [RequireComponent(typeof(AnimationControl))]
    public class SpeakAnimation : MonoBehaviour
    {
        [SerializeField] private AnimationControl _animationControl;
        
        private string _name;
        private EventBus _eventBus;

        public void Construct(EventBus eventBus, string nme)
        {
            _name = nme;
            
            _eventBus = eventBus;
            
            _eventBus.Subscribe<DialogueEventSignal, string>(this, AnimationEvent);
        }

        private void AnimationEvent(string message)
        {
            string[] parameters = GetParams(message);
            if (parameters == null)
                return;

            string nme = parameters[0];

            if (nme != _name)
            {
                return;
            }
            
            string animationName = parameters[1];
            
            if (!_animationControl.HasAnimation(animationName))
            {
                Debug.LogWarning("No animation found for " + animationName);
                
                return;
            }
            
            int layer = int.Parse(parameters[2]);
            int isLoopInt = int.Parse(parameters[3]);
            
            bool isLoop = isLoopInt == 1;
            
            
            
            _animationControl.SetAnimation(layer, animationName, isLoop);
        }

        private string[] GetParams(string message)
        {
            if (message.Contains("a_"))
            {
                message = message.Replace("a_", "");
                
                string[] parameters =  message.Split('_');
                
                return parameters;
            }
            
            return null;
        }
        
        private void OnDestroy()
        {
            _eventBus.Unsubscribe<DialogueEventSignal>(this);
        }
    }
}