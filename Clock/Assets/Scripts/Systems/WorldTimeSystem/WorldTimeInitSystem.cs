using Leopotam.EcsLite;
using LeopotamGroup.Globals;
using UnityEngine;

namespace MSuhinin.Clock
{
    public class WorldTimeInitSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private IWorldTimeService wts;
        private EcsPool<WorldTimeComponent> _worldTimeComponentPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<WorldTimeComponent>().End();
            wts = Service<IWorldTimeService>.Get();
            _worldTimeComponentPool = _world.GetPool<WorldTimeComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                ref var worldTimeComponentPool = ref _worldTimeComponentPool.Get(entity);
                worldTimeComponentPool.DateTime = wts.GetCurrentDateTime();
                Debug.Log(worldTimeComponentPool.DateTime.ToString());
                _worldTimeComponentPool.Del(entity);
            }
        }
    }
}