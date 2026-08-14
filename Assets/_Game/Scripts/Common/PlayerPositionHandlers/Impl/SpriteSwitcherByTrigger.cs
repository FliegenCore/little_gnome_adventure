using _Game.Scripts.InteractionSystems;
using UnityEngine;

namespace _Game.Scripts.Common.Impl
{
    public class SpriteSwitcherByTrigger : MonoBehaviour
    {
        [SerializeField] private ContactTriggerProvider _leftContactTriggerProvider;
        [SerializeField] private ContactTriggerProvider _rightContactTriggerProvider;
        [SerializeField] private SpriteRenderer _leftSprite;
        [SerializeField] private SpriteRenderer _rightSprite;

        private void Awake()
        {
            _leftContactTriggerProvider.OnEnter += EnableLeftSprite;
            _rightContactTriggerProvider.OnEnter += EnableRightSprite;
        }

        private void EnableRightSprite(Collider2D _)
        {
            _leftSprite.gameObject.SetActive(false);
            _rightSprite.gameObject.SetActive(true);
        }

        private void EnableLeftSprite(Collider2D _)
        {
            _leftSprite.gameObject.SetActive(true);
            _rightSprite.gameObject.SetActive(false);
        }
    }
}