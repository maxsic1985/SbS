using System;
using Leopotam.EcsLite;
using UnityEngine;

namespace MSuhinin.Clock
{
    public class ClockLagSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<IsNewHourComponent> _isNewHourComponentPool;
        private EcsPool<IsNeсesaryUpdateTimeFromNet> _isNessesaryUpdateTimeFromNetComponentPool;
        private EcsPool<TimeComponent> _timeComponentPool;
        private EcsPool<WorldTimeComponent> _worldTimeComponentPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _filter = _world
                .Filter<IsWorldTimeComponent>()
                .Inc<WorldTimeComponent>()
                .Inc<IsNewHourComponent>()
                .Inc<TimeComponent>()
                .End();

            _worldTimeComponentPool = _world.GetPool<WorldTimeComponent>();
            _timeComponentPool = _world.GetPool<TimeComponent>();
            _isNewHourComponentPool = _world.GetPool<IsNewHourComponent>();
            _isNessesaryUpdateTimeFromNetComponentPool = _world.GetPool<IsNeсesaryUpdateTimeFromNet>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var time = ref _timeComponentPool.Get(entity);
                ref var wTime = ref _worldTimeComponentPool.Get(entity);


                ref var n = ref _isNessesaryUpdateTimeFromNetComponentPool.Add(entity);

                _isNewHourComponentPool.Del(entity);
            }
        }
    }
}