using UniRx;
using UnityEngine;

namespace Game.PlayerSystem
{
    public class Transformable : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        
        private Transformation _transformation;
        
        public void Construct(Transformation transformation)
        {
            _transformation = transformation;
            
            _transformation.Direction.Subscribe(SetDirection);
            _transformation.Scale.Subscribe(SetScale);
            _transformation.Position.Subscribe(SetPosition);
        }

        private void SetDirection(Vector2 motionDirection)
        {
            Vector2 direction = motionDirection;
            
            _rigidbody.linearVelocity = direction;
            CachePosition();
        }

        private void SetPosition(Vector2 position)
        {
            transform.position = position;
        }

        private void SetScale(Vector3 scale)
        {
            transform.localScale = scale;
        }
        
        private void CachePosition()
        {
            _transformation.Position.ValueWithoutInvoke = transform.position;
        }
        
        private void OnDestroy()
        {
            _transformation.Direction.Unsubscribe(SetDirection);
            _transformation.Scale.Unsubscribe(SetScale);
            _transformation.Position.Unsubscribe(SetPosition);
        }
    }
}