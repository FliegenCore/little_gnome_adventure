using UnityEngine;

namespace _Game.Scripts.Sound
{
    public class AudioSourceStorage : MonoBehaviour
    {
        [SerializeField] private AudioSource _backgroundSource;
        
        
        public void PlayBackgroundSound(AudioClip audioClip)
        {
            _backgroundSource.PlayOneShot(audioClip);
        }    
    }
}