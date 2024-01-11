using Leopotam.EcsLite;



namespace MSuhinin.Clock
{
    public sealed class TestSmoothMovingTimerParkingSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsPool<TransportShipReturnPositionComponent> _transportShipReturnPositionPool;
        private EcsPool<DestinationComponent> _destinationPool;
        private EcsFilter _filter;
        private EcsWorld _world;


        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<IsTimerFinishedComponent>()
                .Inc<IsTransportShipComponent>()
                .End();
            _transportShipReturnPositionPool = _world.GetPool<TransportShipReturnPositionComponent>();
            _destinationPool = _world.GetPool<DestinationComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                _destinationPool = _world.GetPool<DestinationComponent>();
                if (_destinationPool.Has(entity)) _destinationPool.Del(entity);;
                ref var destonation = ref _destinationPool.Add(entity);
                var vectorDestanationBack = _transportShipReturnPositionPool.Get(entity);
                destonation.Value = vectorDestanationBack.Value;
            }
        }
    }
}