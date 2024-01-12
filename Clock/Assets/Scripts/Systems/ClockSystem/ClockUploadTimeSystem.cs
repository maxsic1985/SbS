﻿using Leopotam.EcsLite;
using UnityEngine;


namespace MSuhinin.Clock
{
    public sealed class ClockUploadTimeSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filterClock;
        private EcsFilter _filterWorldTime;
        private EcsPool<TimeComponent> _worldTimeComponentPool;
        private EcsPool<ClockViewComponent> _clockViewComponentPool;
      


        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            
            _filterClock = world.Filter<IsClockComponent>()
                .Inc<ClockViewComponent>()
                .End();
            _filterWorldTime = world.Filter<TimeComponent>().End();
            
            _worldTimeComponentPool = world.GetPool<TimeComponent>();
            _clockViewComponentPool = world.GetPool<ClockViewComponent>();
      
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity1 in _filterClock)
            {
                ref var clockView = ref _clockViewComponentPool.Get(entity1);
                foreach (var entity2 in _filterWorldTime)
                {
                    ref var worldTimeComponentPool = ref _worldTimeComponentPool.Get(entity2);
                    var hour = Mathf.Floor(worldTimeComponentPool.HOUR * GameConstants.HOURS_TO_DEGREES);
                    var min = Mathf.Floor(worldTimeComponentPool.MIN * GameConstants.MINUTES_TO_DEGREES);
                    clockView.HoursEuler.rotation=Quaternion.Euler(0,0, -hour);
                    clockView.MinutesEuler.rotation=Quaternion.Euler(0,0, -min);
                   
                }
            }
        }
    }
}