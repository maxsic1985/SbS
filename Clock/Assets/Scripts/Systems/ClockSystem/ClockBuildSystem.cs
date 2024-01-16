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
        private EcsPool<MouseDirectionComponent> _mouseDirection;


        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _filter = world
                .Filter<IsClockComponent>()
                .Inc<PrefabComponent>().End();
            _prefabPool = world.GetPool<PrefabComponent>();
            _transformComponentPool = world.GetPool<TransformComponent>();
            _clockViewComponentPool = world.GetPool<ClockViewComponent>();
            _mouseDirection = world.GetPool<MouseDirectionComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var prefabComponent = ref _prefabPool.Get(entity);
                ref var transformComponent = ref _transformComponentPool.Add(entity);

                var gameObject = Object.Instantiate(prefabComponent.Value);
                var clockView = gameObject.GetComponent<ClockView>();
                transformComponent.Value = clockView.transform;
                gameObject.transform.position = Vector3.zero;
                
                ref var clockViewComponentPool = ref _clockViewComponentPool.Add(entity);
                clockViewComponentPool.HoursEuler = clockView.HoursTransform.transform;
                clockViewComponentPool.MinutesEuler = clockView.MinutesTransform.transform;
                clockViewComponentPool.SecondsEuler = clockView.SecondsTransform.transform;
                clockViewComponentPool.TextTime = clockView.TextTime;
                clockViewComponentPool.CheckBoxSetTime = clockView.CheckBoxSetTime;

                var world = systems.GetWorld();
              var ne=  world.NewEntity();
                var ddd = gameObject.GetComponentInChildren<HourView>();
                ref var md = ref _mouseDirection.Add(ne);
                md.LastPosition = ddd.BeginEvent;
                md.Position = ddd.DrugEvent;
                md.IsPositive = ddd.Direction;
                
                _prefabPool.Del(entity);
            }
        }
    }
}