using Leopotam.EcsLite;
using UnityEngine;


namespace MSuhinin.Clock
{
    public sealed class ClockBuildSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<PrefabComponent> _prefabPool;
        private EcsPool<TransformComponent> _transformComponentPool;
        private EcsPool<ClockViewComponent> _clockViewComponentPool;


        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _filter = world
                .Filter<IsClockComponent>()
                .Inc<PrefabComponent>().End();
            _prefabPool = world.GetPool<PrefabComponent>();
            _transformComponentPool = world.GetPool<TransformComponent>();
            _clockViewComponentPool = world.GetPool<ClockViewComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var prefabComponent = ref _prefabPool.Get(entity);
                ref var transformComponent = ref _transformComponentPool.Add(entity);

                var gameObject = Object.Instantiate(prefabComponent.Value);
                var view = gameObject.GetComponent<ClockView>();
                transformComponent.Value = view.transform;
                gameObject.transform.position = Vector3.zero;
                
                ref var clockViewComponentPool = ref _clockViewComponentPool.Add(entity);
                clockViewComponentPool.HoursEuler = view.HoursTransform.transform;
                clockViewComponentPool.MinutesEuler = view.MinutesTransform.transform;
                clockViewComponentPool.SecondsEuler = view.SecondsTransform.transform;
                
                _prefabPool.Del(entity);
            }
        }
    }
}