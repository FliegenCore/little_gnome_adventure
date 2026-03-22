using _Game.Scripts.Utils;
using UnityEngine;

namespace _Game.Scripts.RoomSystems.LocationModels
{
    public class LampModel
    {
        public readonly Observable<float> LampLightValue;

        private readonly float _maxValue;
        private readonly float _minValue;
        
        private float _flickerTimer;
        private float _nextFlickerTime;
        private bool _isFlickering;
        private float _flickerDuration;
        private float _targetFlickerValue;

        public LampModel(float minValue, float maxValue)
        {
            LampLightValue = new Observable<float>(minValue);
            _maxValue = maxValue;
            _minValue = minValue;
            
            ResetFlickerTimer();
        }

        public void Update(float deltaTime)
        {
            UpdateFlicker(deltaTime);
            
            if (!_isFlickering)
            {
                UpdateNormalLight(deltaTime);
            }
        }
        
        private void UpdateNormalLight(float deltaTime)
        {
            float change = deltaTime * 0.3f;
            LampLightValue.Value = Mathf.PingPong(Time.time * 0.3f, _maxValue - _minValue) + _minValue;
        }
        
        private void UpdateFlicker(float deltaTime)
        {
            _flickerTimer += deltaTime;
            
            if (_flickerTimer >= _nextFlickerTime && !_isFlickering)
            {
                StartRandomFlicker();
            }
            
            if (_isFlickering)
            {
                _flickerDuration -= deltaTime;
                
                LampLightValue.Value = Mathf.Lerp(LampLightValue.Value, _targetFlickerValue, deltaTime * 10f);
                
                if (_flickerDuration <= 0)
                {
                    _isFlickering = false;
                    ResetFlickerTimer();
                }
            }
        }
        
        private void StartRandomFlicker()
        {
            _isFlickering = true;
            
            _flickerDuration = Random.Range(0.1f, 0.5f);
            
            if (Random.value > 0.5f)
            {
                _targetFlickerValue = Random.Range(_minValue, _minValue + (_maxValue - _minValue) * 0.3f);
            }
            else
            {
                _targetFlickerValue = Random.Range(_maxValue - (_maxValue - _minValue) * 0.3f, _maxValue);
            }
            
            if (Random.value > 0.8f)
            {
                _targetFlickerValue = _minValue;
            }
        }
        
        private void ResetFlickerTimer()
        {
            _nextFlickerTime = Random.Range(0.5f, 3f);
            _flickerTimer = 0f;
        }
    }
}