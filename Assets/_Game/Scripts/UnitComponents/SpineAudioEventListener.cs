using _Game.Scripts.PlayerSystems.Animations;
using _Game.Scripts.Sound;
using Spine;
using UnityEngine;
using VContainer;

namespace Game.PlayerSystem
{
    [RequireComponent(typeof(AnimationControl))]
    public class SpineAudioEventListener : MonoBehaviour
    {
        private ISoundManager _soundManager;
        private AnimationControl _animationControl;
        
        [Inject]
        private void Construct(ISoundManager soundManager)
        {
            _soundManager = soundManager;
            Debug.Log(name);
        }

        private void Start()
        {
            _animationControl = GetComponent<AnimationControl>();
            _animationControl.SubscribeOnEvents(HandleEvent);
        }
        
        private void HandleEvent(TrackEntry trackEntry, Spine.Event e) 
        {
            if (_soundManager.HasSound(e.Data.Name))
            {
                Debug.Log(_soundManager.HasSound(e.Data.Name));
                _soundManager.PlayOnPosition(transform, 25, e.Data.Name, false);
            }
        }

        private void OnDestroy()
        {
            _animationControl.UnsubscribeOnEvents(HandleEvent);
        }
    }
}