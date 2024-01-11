using Leopotam.EcsLite;



namespace MSuhinin.Clock
{
    public sealed class TestSmoothMovingCheckArrivalSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsPool<DestinationComponent> _destinationPool;
        private EcsPool<PositionComponent> _positionPool;
        private EcsFilter _filter;
        private EcsWorld _world;
        

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<DestinationComponent>()
                .Inc<SmoothStepMovingComponent>()
                .Inc<PositionComponent>()
                .End();
            _destinationPool = _world.GetPool<DestinationComponent>();
            _positionPool = _world.GetPool<PositionComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var position = ref _positionPool.Get(entity);
                ref var destination = ref _destinationPool.Get(entity);
                var heading = destination.Value - position.Value;
                if (heading.sqrMagnitude < LimitsConstants.STOP_THRESHOLD * LimitsConstants.STOP_THRESHOLD)
                {
                    _destinationPool.Del(entity);
                }
            }
        }
    }
}