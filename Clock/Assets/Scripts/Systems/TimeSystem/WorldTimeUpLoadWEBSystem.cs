using System;
using Leopotam.EcsLite;
using LeopotamGroup.Globals;
using UnityEngine;


namespace MSuhinin.Clock
{
    public sealed class WorldTimeUpLoadWEBSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private IWorldTimeService _worldTimeService;
        private EcsPool<WorldTimeComponent> _worldTimeComponentPool;
        private EcsPool<TimeComponent> _timeComponentPool;
        private EcsPool<IsWorldTimeComponent> _isGetWorldTimeComponent;
        private EcsPool<IsNeсesaryUpdateTimeFromNet> _isNessesaryUpdateTimeFromNetComponentPool;


        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<IsWorldTimeComponent>()
                .Inc<WorldTimeComponent>()
                .Inc<IsNeсesaryUpdateTimeFromNet>()
                .End();
            _worldTimeService = Service<IWorldTimeService>.Get();
            _worldTimeComponentPool = _world.GetPool<WorldTimeComponent>();
            _timeComponentPool = _world.GetPool<TimeComponent>();
            _isGetWorldTimeComponent = _world.GetPool<IsWorldTimeComponent>();
            _isNessesaryUpdateTimeFromNetComponentPool = _world.GetPool<IsNeсesaryUpdateTimeFromNet>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                ref var worldTimeComponentPool = ref _worldTimeComponentPool.Get(entity);
                worldTimeComponentPool.DateTime = _worldTimeService.GetCurrentDateTime();


                ref var tc = ref _timeComponentPool.Get(entity);
                if (tc.HOUR != worldTimeComponentPool.DateTime.Hour
                    || tc.MIN != worldTimeComponentPool.DateTime.Minute
                    || Math.Abs(tc.SEC - worldTimeComponentPool.DateTime.Second) > GameConstants.TIME_LAG)
                {
                    tc.HOUR = worldTimeComponentPool.DateTime.Hour;
                    tc.MIN = worldTimeComponentPool.DateTime.Minute;
                    tc.SEC = worldTimeComponentPool.DateTime.Second;
                }

                Debug.Log(worldTimeComponentPool.DateTime.ToString());

                //_isGetWorldTimeComponent.Del(entity);
                _isNessesaryUpdateTimeFromNetComponentPool.Del(entity);
            }
        }
    }
}