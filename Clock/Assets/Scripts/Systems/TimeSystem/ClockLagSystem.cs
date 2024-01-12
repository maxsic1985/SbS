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

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _filter = _world
                .Filter<IsWorldTimeComponent>()
                .Inc<WorldTimeComponent>()
                .Inc<IsNewHourComponent>()
                .End();
            
            _isNewHourComponentPool = _world.GetPool<IsNewHourComponent>();
            _isNessesaryUpdateTimeFromNetComponentPool = _world.GetPool<IsNeсesaryUpdateTimeFromNet>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var n = ref _isNessesaryUpdateTimeFromNetComponentPool.Add(entity);
                _isNewHourComponentPool.Del(entity);
            }
        }
    }
}