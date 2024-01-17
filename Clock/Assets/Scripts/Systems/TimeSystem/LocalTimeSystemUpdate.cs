using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using UniRx;


namespace MSuhinin.Clock
{
    public sealed class LocalTimeSystemUpdate : IEcsInitSystem,IEcsDestroySystem
    {
        private EcsFilter _filter;
        private EcsPool<TimeComponent> _timeComponentPool;
        private EcsPool<IsNewHourComponent> _isNewHourComponentPool;
        
        private List<IDisposable> _disposables = new List<IDisposable>();


        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _filter = world
                .Filter<TimeComponent>().End();
            _timeComponentPool = world.GetPool<TimeComponent>();
            _isNewHourComponentPool = world.GetPool<IsNewHourComponent>();

            Observable.Interval(TimeSpan.FromMilliseconds(GameConstants.ONE_SECOND_IN_MS))
                .Where(_ => true).Subscribe(x => { UpdateTime(); })
                .AddTo(_disposables);
        }

        private void UpdateTime()
        {
            foreach (var entity in _filter)
            {
                UpdateHandsClock(entity);
            }
        }

        private void UpdateHandsClock(int entity)
        {
            ref var time = ref _timeComponentPool.Get(entity);
            time.SEC++;
            if (time.SEC >= GameConstants.MIN_SEC_DURATION
                && time.MIN < GameConstants.MIN_SEC_DURATION)
            {
                time.MIN++;
                time.SEC = 0;
            }
            else if (time.SEC >= GameConstants.MIN_SEC_DURATION
                     && time.MIN == GameConstants.MIN_SEC_DURATION)
            {
                time.MIN = 0;
                time.SEC = 0;
                var tcHour = time.HOUR == GameConstants.HOUR_DURATIO ? time.HOUR = 0 : time.HOUR++;
                
                ref var n = ref _isNewHourComponentPool.Add(entity);
            }

          
        }

        public void Destroy(IEcsSystems systems)
        {
            Dispose();
        }

        private void Dispose()
        {
            _disposables.Clear();
        }
    }
}