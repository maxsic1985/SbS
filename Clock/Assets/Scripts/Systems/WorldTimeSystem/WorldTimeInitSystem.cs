using Leopotam.EcsLite;

namespace MSuhinin.Clock
{
    public sealed class WorldTimeInitSystem: IEcsInitSystem
    {
        private EcsPool<IsWorldTimeComponent> _isWorldTimeComponentPool;
        private EcsPool<WorldTimeComponent> _worldTimeComponentPool;
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var entity = world.NewEntity();
            
            _isWorldTimeComponentPool = world.GetPool<IsWorldTimeComponent>();
            _isWorldTimeComponentPool.Add(entity);
            _worldTimeComponentPool = world.GetPool<WorldTimeComponent>();
            _worldTimeComponentPool.Add(entity);
            //    var loadDataByNameComponent = world.GetPool<LoadDataByNameComponent>();
            //   ref var loadFactoryDataComponent = ref loadDataByNameComponent.Add(entity);
            //    loadFactoryDataComponent.AddressableName = AssetsNamesConstants.CLOCK_PREFAB;
        }
    }
}