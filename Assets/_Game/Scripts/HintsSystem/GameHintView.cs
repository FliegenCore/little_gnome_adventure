using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.HintsSystem
{
    public class GameHintView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _hintText;

        private Tween _showTween;

        public void HideHintText()
        {
            _hintText.gameObject.SetActive(false);
        }

        public void ShowHintWithDelay(string text, float delay = 0.5f)
        {
            _showTween?.Kill();
            _hintText.text = text;
            Color color = _hintText.color;
            
            _hintText.color = new Color(color.r, color.g, color.b, 0);
            _hintText.gameObject.SetActive(true);
            
            _showTween = _hintText.DOFade(1f, delay);
        }

        public void HideCurrentHintWithDelay(float delay = 0.25f)
        {
            _showTween?.Kill();
            
            _showTween = _hintText.DOFade(0f, delay).OnComplete(() =>
            {
                _hintText.gameObject.SetActive(false);
            });
        }
    }
}