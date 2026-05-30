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
            _eventBus.Subscribe<DialogueEventWithCallbackSignal, string, Action>(this, AnimationEventWithEvent);
        }

        private void AnimationEventWithEvent(string message, Action action)
        {
            string[] parameters = GetParams(message, out var _);
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

            int layer = 0;
            int isLoopInt = 0;
            
            if (parameters.Length > 2)
                layer = int.Parse(parameters[2]);

            if (parameters.Length > 3)
                isLoopInt = int.Parse(parameters[3]);
            
            bool isLoop = isLoopInt == 1;
            
            _animationControl.SetAnimation(layer, animationName, isLoop, action);
        }
        
        private void AnimationEvent(string message)
        {
            string[] parameters = GetParams(message, out string secondEvent);
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

            int layer = 0;
            int isLoopInt = 0;

            string exitAnim = string.Empty;
            
            if (parameters.Length > 2)
                layer = int.Parse(parameters[2]);

            if (parameters.Length > 3)
                isLoopInt = int.Parse(parameters[3]);
            
            if (parameters.Length > 4)
                exitAnim = parameters[4];
            
            bool isLoop = isLoopInt == 1;

            if (secondEvent != string.Empty)
            {
                _animationControl.SetAnimation(layer, animationName, isLoop, () =>
                {
                    if (!string.IsNullOrEmpty(exitAnim))
                    {
                        _animationControl.SetAnimation(layer, exitAnim, true, () =>
                        {
                            _eventBus.TriggerEvenet<DialogueEventSignal, string>(secondEvent);
                            return;
                        });
                    }
                    
                    _eventBus.TriggerEvenet<DialogueEventSignal, string>(secondEvent);
                });
            }
            else
            {
                _animationControl.SetAnimation(layer, animationName, isLoop, () =>
                {
                    if (!string.IsNullOrEmpty(exitAnim))
                    {
                        _animationControl.SetAnimation(layer, exitAnim);
                    }
                });
            }
        }

        private string[] GetParams(string message, out string secondEvent)
        {
            secondEvent = string.Empty;
    
            int startIndex = message.IndexOf('(');
            int endIndex = message.IndexOf(')');
    
            if (startIndex != -1 && endIndex != -1 && endIndex > startIndex)
            {
                secondEvent = message.Substring(startIndex + 1, endIndex - startIndex - 1);
        
                if (!string.IsNullOrEmpty(secondEvent))
                {
                    string partToRemove = message.Substring(startIndex, endIndex - startIndex + 1);
                    message = message.Replace(partToRemove, "");
                }
            }
    
            if (message.Contains("a_"))
            {
                message = message.Replace("a_", "");
        
                string[] parameters = message.Split('_', StringSplitOptions.RemoveEmptyEntries);
        
                return parameters;
            }
    
            return null;
        }
        
        private void OnDestroy()
        {
            if (_eventBus != null)
            {
                _eventBus.Unsubscribe<DialogueEventSignal>(this);
                _eventBus.Unsubscribe<DialogueEventWithCallbackSignal>(this);
            }
        }
    }
}