using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.DialogueSystem.View
{
    public class SpeakWindowView : MonoBehaviour
    {
        private const float _size = 0.001501502f;
        
        private Sequence _animationSequence;
        
        [field: SerializeField] public TMP_Text RealText { get; private set; }
        [field: SerializeField] public TMP_Text FakeText { get; private set; }
        
        
        public void Hide()
        {
            if(_animationSequence != null)
                _animationSequence.Kill();
            
            _animationSequence = DOTween.Sequence();

            _animationSequence.Append(transform.DOScale(Vector3.zero, 0.25f)).OnComplete(() => gameObject.SetActive(false));
            
        }

        public void Show()
        {
            gameObject.SetActive(true);
            if(_animationSequence != null)
                _animationSequence.Kill();
            
            _animationSequence = DOTween.Sequence();
            _animationSequence.Append(transform.DOScale(new Vector3(_size,_size,_size), 0.25f).From(Vector3.zero));
        }
    }
}