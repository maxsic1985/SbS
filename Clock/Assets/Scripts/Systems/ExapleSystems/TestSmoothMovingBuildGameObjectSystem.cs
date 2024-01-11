using Leopotam.EcsLite;
using UnityEngine;



namespace MSuhinin.Clock
{
    public class TestSmoothMovingBuildGameObjectSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<PrefabComponent> _prefabPool;
        private EcsPool<TransformComponent> _transformPool;


        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _filter = world.Filter<IsTransportShipComponent>().Inc<PrefabComponent>().End();
            _prefabPool = world.GetPool<PrefabComponent>();
            _transformPool = world.GetPool<TransformComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var gameObjectComponent = ref _prefabPool.Get(entity);
                var gameObject = Object.Instantiate(gameObjectComponent.Value);
                ref var transformComponent = ref _transformPool.Add(entity);
                transformComponent.Value = gameObject.GetComponent<TransformView>().Transform;
                _prefabPool.Del(entity);
            }
        }
    }
}