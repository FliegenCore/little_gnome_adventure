using System;
using _Game.Scripts.Helpers;
using UnityEngine;
using VContainer.Unity;
using EventBus = Core.Common.EventBus;

namespace _Game.Scripts.HintsSystem
{
    public class GameHintsService : IInitializable, IDisposable
    {
        private readonly GameHintsConfig _gameHintsConfig;
        private readonly GameHintView _gameHintView;
        private readonly EventBus _eventBus;

        private GameHintsService(GameHintsConfig  gameHintsConfig, GameHintView gameHintView, EventBus eventBus)
        {
            _gameHintsConfig = gameHintsConfig;
            _gameHintView    = gameHintView;
            _eventBus        = eventBus;
        }
        
        public void Initialize()
        {
            _eventBus.Subscribe<ShowGameHintSignal, EHintType>(this, ShowHint);
            _eventBus.Subscribe<HideGameHintSignal>(this, HideHint);

            _gameHintView.HideHintText();
        }
        
        public void ShowHint(EHintType hintType)
        {
            string text = GetText(hintType);
            
            _gameHintView.ShowHintWithDelay(text.PutBrackets());
        }

        public string GetText(EHintType hintType)
        {
            return _gameHintsConfig.GetText(hintType);
        }

        public void HideHint()
        {
            _gameHintView.HideCurrentHintWithDelay();
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<ShowGameHintSignal>(this);
            _eventBus.Unsubscribe<HideGameHintSignal>(this);
        }
    }
}