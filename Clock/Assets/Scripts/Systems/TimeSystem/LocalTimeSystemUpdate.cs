using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Leopotam.EcsLite;
using UniRx;
using UnityEditor.Timeline.Actions;
using UnityEngine;


namespace MSuhinin.Clock
{
    public sealed class LocalTimeSystemUpdate : IEcsInitSystem
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
  
            Observable.Interval(TimeSpan.FromMilliseconds(1000))
                .Where(_ => true).Subscribe(x =>
                {
                    UpdateTime();
                })
                .AddTo(_disposables);
            
        }

        private void UpdateTime()
        {
            foreach (var entity in _filter)
            {
                ref var tc = ref _timeComponentPool.Get(entity);
                tc.SEC++;
                if (tc.SEC>=59 && tc.MIN<59)
                {
                    tc.MIN++;
                    tc.SEC = 0;
                }
                else if (tc.SEC>=59 && tc.MIN==59)
                {
                    tc.MIN=0;
                    tc.SEC = 0;
                    var tcHour = tc.HOUR==23 ? tc.HOUR=0 : tc.HOUR++;
                }
            }
        }

      
    }
}