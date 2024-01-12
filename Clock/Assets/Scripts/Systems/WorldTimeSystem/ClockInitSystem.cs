using Leopotam.EcsLite;

namespace MSuhinin.Clock
{
    public sealed class ClockInitSystem: IEcsInitSystem
    {
        private EcsPool<IsGetWorldTimeComponent> _isGetWorldTimeComponentPool;
        private EcsPool<WorldTimeComponent> _worldTimeComponentPool;
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var entity = world.NewEntity();
            
            _isGetWorldTimeComponentPool = world.GetPool<IsGetWorldTimeComponent>();
            _worldTimeComponentPool = world.GetPool<WorldTimeComponent>();
            _isGetWorldTimeComponentPool.Add(entity);
            _worldTimeComponentPool.Add(entity);

            var loadDataByNameComponent = world.GetPool<LoadDataByNameComponent>();
            ref var loadFactoryDataComponent = ref loadDataByNameComponent.Add(entity);
            loadFactoryDataComponent.AddressableName = AssetsNamesConstants.CLOCK_PREFAB;
        }
    }
}