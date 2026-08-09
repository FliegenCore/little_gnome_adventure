using System;
using System.Collections.Generic;
using _Game.Scripts.InventorySystem;
using Core.Common;
using UnityEngine;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace _Game.Scripts.RoomSystems.InputInfoSystem
{
    public class InputInfoManager : IInitializable, IDisposable
    {
        private readonly InputInfoView _inputInfoView;
        private readonly InputInfoConfig _inputInfoConfig;
        private readonly EventBus _eventBus;

        private List<InputInfoGroupView> _inputInfoGroupViews = new();
        
        private InputInfoManager(
            InputInfoView infoView,
            InputInfoConfig config,
            EventBus eventBus
            )
        {
            _eventBus        = eventBus;
            _inputInfoConfig = config;
            _inputInfoView   = infoView;
        }
        
        public void Initialize()
        {
            _eventBus.Subscribe<ShowInputInfoViewSignal, InputInfoGroup[]>(this, ShowInfo);
            _eventBus.Subscribe<HideInputInfoViewSignal>(this, DestroyAllInfo);
        }
        
        private Sprite GetInputSprite(EKeyIndex index) => _inputInfoConfig.GetSprite(index);

        private void ShowInfo(params InputInfoGroup[] inputInfoGroups)
        {
            DestroyAllInfo();
            
            foreach (var infoGroup in inputInfoGroups)
            {
                InputInfoGroupView infoGroupView = CreateInputInfoGroupView(infoGroup.Description, infoGroup.KeyIndices);
                _inputInfoGroupViews.Add(infoGroupView);
            }
        }

        private void DestroyAllInfo()
        {
            foreach (InputInfoGroupView inputInfoGroup in _inputInfoGroupViews)
            {
                Object.Destroy(inputInfoGroup.gameObject);
            }
            
            _inputInfoGroupViews.Clear();
        }

        private InputInfoGroupView CreateInputInfoGroupView(string description, params EKeyIndex[] keys)
        {
            InputInfoGroupView infoGroupView =
                Object.Instantiate(_inputInfoView.InputInfoGroupViewPrefab, _inputInfoView.transform);
            
            infoGroupView.SetDescription(description);
            
            int sibilingIndex = 0;
            
            foreach (var key in keys)
            {
                Sprite sprite = GetInputSprite(key);
                SpriteStorage storage = new SpriteStorage(sprite);

                SpriteApplyer spriteApplyer = Object.Instantiate(_inputInfoView.InputImageSpriteApplyerPrefab, infoGroupView.transform);
                spriteApplyer.Construct(storage, new Vector2(50f, 50f));
                spriteApplyer.transform.SetSiblingIndex(sibilingIndex);
                sibilingIndex++;
            }
            
            return infoGroupView;
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<ShowInputInfoViewSignal>(this);
            _eventBus.Unsubscribe<HideInputInfoViewSignal>(this);
        }

        
    }
}