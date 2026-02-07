using System.Collections;
using _Game.Scripts.UpdateSystems;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace _Game.Scripts.Common
{
    public class LightBlicker : MonoBehaviour
    {
        [SerializeField] private Light2D _light;
        
        public void SetLightIntensity(float intensity)
        {
            _light.intensity = intensity;
        }
    }
}