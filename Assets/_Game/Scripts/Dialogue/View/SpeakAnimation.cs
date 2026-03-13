using System;
using _Game.Scripts.PlayerSystems.Animations;
using Core.Common;
using UnityEngine;

namespace _Game.Scripts.DialogueSystem.View
{
    public class SpeakAnimation : MonoBehaviour
    {
        [SerializeField] private AnimationControl _animationControl;
        
        private EventBus _eventBus;
        private string _name;

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
            string animationName = parameters[1];
            int layer = int.Parse(parameters[2]);
            
            if (nme != _name)
            {
                return;
            }
            
            _animationControl.SetLoopAnimation(layer, animationName);
        }

        private string[] GetParams(string message)
        {
            if (message.Contains("animation_"))
            {
                message = message.Replace("animation_", "");
                
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