using Leopotam.EcsLite;
using UnityEngine;



namespace MSuhinin.Clock
{
    public sealed class TestSmoothMovingSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsPool<DestinationComponent> _destinationPool;
        private EcsPool<PositionComponent> _positionPool;
        private EcsPool<SmoothStepMovingComponent> _smoothStepMovingPool;
        private EcsFilter _filter;
        private EcsWorld _world;
        private float _shipSpeed;
        

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<DestinationComponent>()
                .Inc<IsTransportShipComponent>()
                .Inc<PositionComponent>()
                .End();
            _destinationPool = _world.GetPool<DestinationComponent>();
            _positionPool = _world.GetPool<PositionComponent>();
            _smoothStepMovingPool = _world.GetPool<SmoothStepMovingComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var smoothStepMoving = ref _smoothStepMovingPool.Get(entity);
                _shipSpeed = smoothStepMoving.Value;

                float time = Time.deltaTime * _shipSpeed;
                ref var shipPosition = ref _positionPool.Get(entity);
                ref var destinationPosition = ref _destinationPool.Get(entity);

                shipPosition.Value = new Vector2(
                    (Mathf.SmoothStep(shipPosition.Value.x, destinationPosition.Value.x, time)),
                    (Mathf.SmoothStep(shipPosition.Value.y, destinationPosition.Value.y, time)));
            }
        }
    }
}