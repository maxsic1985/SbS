using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Leopotam.EcsLite;
using UniRx;
using UnityEditor.Timeline.Actions;
using UnityEngine;


namespace MSuhinin.Clock
{
    public sealed class LocalTimeSystemUpdate : IEcsInitSystem,IEcsDestroySystem
    {
        private EcsFilter _filter;
        private EcsPool<TimeComponent> _timeComponentPool;
        private List<IDisposable> _disposables = new List<IDisposable>();


        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _filter = world
                .Filter<TimeComponent>().End();
            _timeComponentPool = world.GetPool<TimeComponent>();

            Observable.Interval(TimeSpan.FromMilliseconds(GameConstants.ONE_SECOND_IN_MS))
                .Where(_ => true).Subscribe(x => { UpdateTime(); })
                .AddTo(_disposables);
        }

        private void UpdateTime()
        {
            foreach (var entity in _filter)
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
                }
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