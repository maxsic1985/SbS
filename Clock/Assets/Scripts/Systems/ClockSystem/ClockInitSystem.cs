using Leopotam.EcsLite;

namespace MSuhinin.Clock
{
    public sealed class ClockInitSystem: IEcsInitSystem
    {
        private EcsPool<IsClockComponent> _isClockComponentPool;
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var entity = world.NewEntity();
            
            _isClockComponentPool = world.GetPool<IsClockComponent>();
            _isClockComponentPool.Add(entity);

            var loadDataByNameComponent = world.GetPool<LoadDataByNameComponent>();
            ref var loadFactoryDataComponent = ref loadDataByNameComponent.Add(entity);
            loadFactoryDataComponent.AddressableName = AssetsNamesConstants.CLOCK_PREFAB;
        }
    }
}