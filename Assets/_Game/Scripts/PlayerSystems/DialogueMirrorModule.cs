using System;
using UnityEngine;

namespace _Game.Scripts.PlayerSystems
{
    public class DialogueMirrorModule : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private Transform _dialogueView;

        private bool _isRight = true;
        
        private void Update()
        {
            if (_player.localScale.x == -1 && _isRight)
            {
                _isRight = false;
                _dialogueView.localScale = new Vector3(-1, 1, 1);
                _dialogueView.localPosition = new Vector3(_dialogueView.localPosition.x * -1, _dialogueView.localPosition.y, 0);
            }
            else if(_player.localScale.x == 1 && !_isRight)
            {
                _isRight = true;
                _dialogueView.localScale = new Vector3(1, 1, 1);
                _dialogueView.localPosition = new Vector3(_dialogueView.localPosition.x * -1, _dialogueView.localPosition.y, 0);
            }
        }
    }
}