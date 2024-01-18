using System;
using Leopotam.EcsLite;
using LeopotamGroup.Globals;
using UnityEngine;


namespace MSuhinin.Clock
{
    public sealed class WebUpLoadSystem : IEcsInitSystem, IEcsRunSystem
    {
        private IWorldTimeService _worldTimeService;
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<DateTimeComponent> _worldTimeComponentPool;
        private EcsPool<TimeComponent> _timeComponentPool;
        private EcsPool<IsWorldTimeComponent> _isGetWorldTimeComponent;
        private EcsPool<IsGetUpdateTimeFromNet> _isNessesaryUpdateTimeFromNetComponentPool;


        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<IsWorldTimeComponent>()
                .Inc<DateTimeComponent>()
                .Inc<IsGetUpdateTimeFromNet>()
                .End();
            _worldTimeService = Service<IWorldTimeService>.Get();
            _worldTimeComponentPool = _world.GetPool<DateTimeComponent>();
            _timeComponentPool = _world.GetPool<TimeComponent>();
            _isGetWorldTimeComponent = _world.GetPool<IsWorldTimeComponent>();
            _isNessesaryUpdateTimeFromNetComponentPool = _world.GetPool<IsGetUpdateTimeFromNet>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                ref var worldTimeComponentPool = ref _worldTimeComponentPool.Get(entity);
                worldTimeComponentPool.DateTime = _worldTimeService.UpdateTimeFromWeb();


                ref var time = ref _timeComponentPool.Get(entity);
                
                if (time.HOUR != worldTimeComponentPool.DateTime.Hour
                    || time.MIN != worldTimeComponentPool.DateTime.Minute
                    || Math.Abs(time.SEC - worldTimeComponentPool.DateTime.Second) > GameConstants.TIME_LAG)
                {
                    time.HOUR = worldTimeComponentPool.DateTime.Hour;
                    time.MIN = worldTimeComponentPool.DateTime.Minute;
                    time.SEC = worldTimeComponentPool.DateTime.Second;
                }
                
                _isNessesaryUpdateTimeFromNetComponentPool.Del(entity);
            }
        }
    }
}