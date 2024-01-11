using Leopotam.EcsLite;



namespace MSuhinin.Clock
{
    public class TestSmoothMovingInitSystem : IEcsInitSystem, IEcsRunSystem
    {
             private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<ScriptableObjectComponent> _scriptableObjectPool;
        private EcsPool<LoadPrefabComponent> _loadPrefabPool;
        private EcsPool<TransportShipSpawnPositionComponent> _shipSpawnPositionPool;
        private EcsPool<TransportShipParkingPositionComponent> _shipParkingPositionPool;
        private EcsPool<TransportShipReturnPositionComponent> _shipReturnPositionPool;
        private EcsPool<SmoothStepMovingComponent> _stepMovingPool;
        private EcsPool<PositionComponent> _positionComponentPool;
        private EcsPool<DestinationComponent> _destanationPool;


        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<IsTransportShipComponent>().Inc<ScriptableObjectComponent>().End();
            _scriptableObjectPool = _world.GetPool<ScriptableObjectComponent>();
            _loadPrefabPool = _world.GetPool<LoadPrefabComponent>();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                if (_scriptableObjectPool.Get(entity).Value is ExampleData dataInit)
                {
                    ref var loadPrefabFromPool = ref _loadPrefabPool.Add(entity);
                    loadPrefabFromPool.Value = dataInit.TransportShipPrefab;

                    var timerPool = _world.GetPool<TimerComponent>();
                    ref var timer = ref timerPool.Add(entity);
                    timer.Value = dataInit.exampleMovingData.TimeToStayNearDock;
                    
                    InitShipMovingComponents(entity, dataInit);
                }
                _scriptableObjectPool.Del(entity);
            }
        }

        private void InitShipMovingComponents(int entity, ExampleData dataInit)
        {
            _shipSpawnPositionPool = _world.GetPool<TransportShipSpawnPositionComponent>();
            ref var shipSpawnPosition = ref _shipSpawnPositionPool.Add(entity);
            shipSpawnPosition.Value = dataInit.exampleMovingData.ShipSpawnPosition;

            _shipParkingPositionPool = _world.GetPool<TransportShipParkingPositionComponent>();
            ref var shipParkingPosition = ref _shipParkingPositionPool.Add(entity);
            shipParkingPosition.Value = dataInit.exampleMovingData.ShipParkingPosition;

            _shipReturnPositionPool = _world.GetPool<TransportShipReturnPositionComponent>();
            ref var shipReturnPosition = ref _shipReturnPositionPool.Add(entity);
            shipReturnPosition.Value = dataInit.exampleMovingData.ShipReturnPosition;

            _stepMovingPool = _world.GetPool<SmoothStepMovingComponent>();
            ref var stepMoving = ref _stepMovingPool.Add(entity);
            stepMoving.Value = dataInit.exampleMovingData.ShipSpeed;

            _positionComponentPool = _world.GetPool<PositionComponent>();
            ref var positionComponent = ref _positionComponentPool.Add(entity);
            positionComponent.Value = shipSpawnPosition.Value;

            _destanationPool = _world.GetPool<DestinationComponent>();
            ref var destanation = ref _destanationPool.Add(entity);
            destanation.Value = shipParkingPosition.Value;
        }
    }
}