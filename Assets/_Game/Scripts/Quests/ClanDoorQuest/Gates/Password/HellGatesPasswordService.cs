using System;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.InspectSystem;
using _Game.Scripts.PlayerSystems.PlayerStates;
using _Game.Scripts.Quests.ClanDoorQuest.Gates.Signals;
using Core.Common;
using UniRx;

namespace _Game.Scripts.Quests.ClanDoorQuest.Gates
{
    public class HellGatesPasswordService : IDisposable
    {
        private const string RIGHT_PASSWORD = "123456";
        
        private readonly HellGatesPasswordModel _hellGatesPasswordModel;
        private readonly HellGatesModel _hellGatesModel;
        private readonly EventBus _eventBus;
        
        private readonly CompositeDisposable _disposables = new CompositeDisposable();
        
        public HellGatesPasswordService(
            HellGatesPasswordModel hellGatesPasswordModel,
            HellGatesModel hellGatesModel,
            EventBus eventBus
            )
        {
            _hellGatesModel           = hellGatesModel;
            _eventBus                 = eventBus;
            _hellGatesPasswordModel   = hellGatesPasswordModel;
        }

        public void Initialize()
        {
            _hellGatesPasswordModel.CurrentPassword
                .Subscribe(CompletePassword)
                .AddTo(_disposables);
        }
        
        private void CompletePassword(string currentPassword)
        {
            int currentLength = currentPassword.Length;
            
            if (currentLength == RIGHT_PASSWORD.Length)
            {
                HandlePassword(currentPassword);
            }
        }

        private void HandlePassword(string password)
        {
            _eventBus.TriggerEvenet<SetPlayerStateSignal, Type>(typeof(PlayerNoneState));

            Observable.Timer(TimeSpan.FromSeconds(0.25f)).Subscribe(_ =>
            {
                if (password == RIGHT_PASSWORD)
                {
                    _hellGatesPasswordModel.PublicWriteLock = true;
                
                    _eventBus.TriggerEvenet<AcceptAnimationHellGatesPasswordSignal, Action>(() =>
                    {
                        _hellGatesPasswordModel.PublicWriteLock = true;
                        Observable.Timer(TimeSpan.FromSeconds(0.25f)).Subscribe(_ =>
                        {
                            _eventBus.TriggerEvenet<HideInspectWindowSignal>();
                            _hellGatesPasswordModel.CurrentPassword.Value = string.Empty;
                            _hellGatesModel.CanInteract = false;
                            _hellGatesModel.CanSelected.Value = false;
                            _hellGatesModel.IsSelected.Value = false;

                            Observable.Timer(TimeSpan.FromSeconds(0.25f)).Subscribe(_ =>
                            {
                                _eventBus.TriggerEvenet<SetPlayerStateSignal, Type>(typeof(PlayerBaseState));
                            });

                            _hellGatesModel.OpenDoor?.OnNext(null);
                        });
                    });
                }
                else
                {
                    _hellGatesPasswordModel.PublicWriteLock = true;
                    _hellGatesPasswordModel.CurrentPassword.Value = string.Empty;
                    
                    _eventBus.TriggerEvenet<RejectAnimationHellGatesPasswordSignal, Action>(() =>
                    {
                        _eventBus.TriggerEvenet<SetPlayerStateSignal, Type>(typeof(PlayerInspectState));
                        _hellGatesPasswordModel.PublicWriteLock = false;
                        _hellGatesPasswordModel.WritedCount.Value = 0;
                    });
                }
            });
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}