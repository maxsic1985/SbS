using Leopotam.EcsLite;
using UnityEngine;


namespace MSuhinin.Clock
{
    public sealed class ClockBuildSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<PrefabComponent> _prefabPool;
        private EcsPool<TransformComponent> _transformComponentPool;


        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _filter = world
                .Filter<IsClockComponent>()
                .Inc<PrefabComponent>().End();
            _prefabPool = world.GetPool<PrefabComponent>();
            _transformComponentPool = world.GetPool<TransformComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var prefabComponent = ref _prefabPool.Get(entity);
                ref var transformComponent = ref _transformComponentPool.Add(entity);

                var gameObject = Object.Instantiate(prefabComponent.Value);
                transformComponent.Value = gameObject.GetComponent<ClockView>().transform;
                gameObject.transform.position = Vector3.zero;
                
                _prefabPool.Del(entity);
            }
        }
    }
}