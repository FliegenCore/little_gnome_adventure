using UnityEngine;
using VContainer;

namespace _Game.Scripts.ParalaxSystem
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField, Range(-1f, 1f)] private float _parallaxStrength;
        
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
            
            transform.position += new Vector3(delta.x, 0,0) * _parallaxStrength;
        }
    }
}