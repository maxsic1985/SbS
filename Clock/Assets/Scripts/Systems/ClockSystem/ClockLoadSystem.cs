using Leopotam.EcsLite;
using LeopotamGroup.Globals;
using UnityEngine;


namespace MSuhinin.Clock
{
    public sealed class ClockLoadSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<IsClockComponent> _isClockComponent;
        private EcsPool<ClockTypeComponent> _clockTypeComponent;
        private EcsPool<ScriptableObjectComponent> _scriptableObjectPool;
        private EcsPool<LoadPrefabComponent> _loadPrefabPool;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<IsClockComponent>()
                .Inc<ScriptableObjectComponent>()
                .End();
            _loadPrefabPool = _world.GetPool<LoadPrefabComponent>();
            _isClockComponent = _world.GetPool<IsClockComponent>();
            _clockTypeComponent = _world.GetPool<ClockTypeComponent>();
            _scriptableObjectPool = _world.GetPool<ScriptableObjectComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                if (_scriptableObjectPool.Get(entity).Value is ClockBuildData dataInit)
                {
                    ref LoadPrefabComponent loadPrefabFromPool = ref _loadPrefabPool.Add(entity);
                    loadPrefabFromPool.Value = dataInit.ClockPrefab;
                    
                    ref ClockTypeComponent clockTypeComponent = ref _clockTypeComponent.Add(entity);
                    clockTypeComponent.ClockType = dataInit.ClockType;
                }
                _scriptableObjectPool.Del(entity);
            }
        }
    }
}