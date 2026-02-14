using UnityEngine;
using VContainer;

namespace _Game.Scripts.ParalaxSystem
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)] private float _parallaxStrength;
        
        private Camera _camera;
        private Vector3 _startPosition;
        
        [Inject]
        private void Construct(Camera camera)
        {
            _camera = camera;
            _startPosition = transform.position;
        }

        private void Update()
        {
            var delta = _camera.transform.position - _startPosition;

            _startPosition = _camera.transform.position;
            
            transform.position += delta * _parallaxStrength;
        }
    }
}