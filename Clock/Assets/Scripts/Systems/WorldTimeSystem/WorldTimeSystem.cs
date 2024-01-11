using Leopotam.EcsLite;

namespace MSuhinin.Clock
{
    public class WorldTimeSystem: IEcsInitSystem
    {
        private EcsPool<WorldTimeComponent> _worldTimeComponentPool;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var entity = world.NewEntity();
            
            _worldTimeComponentPool = world.GetPool<WorldTimeComponent>();
            _worldTimeComponentPool.Add(entity);

          //  var loadDataByNameComponent = world.GetPool<LoadDataByNameComponent>();
          //  ref var loadFactoryDataComponent = ref loadDataByNameComponent.Add(entity);
          //  loadFactoryDataComponent.AddressableName = AssetsNamesConstants.CAMERA_PREFAB_NAME;
        }
    }
}