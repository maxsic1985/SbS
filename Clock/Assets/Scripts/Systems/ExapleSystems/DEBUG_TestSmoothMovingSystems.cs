using Leopotam.EcsLite;
using UnityEngine;



namespace MSuhinin.Clock
{
    public class DEBUG_TestSmoothMovingSystems : IEcsRunSystem
    {
        private EcsPool<IsTransportShipComponent> _isTransportShipPool;


        public void Run(IEcsSystems systems)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                var world = systems.GetWorld();
                var entity = world.NewEntity();

                _isTransportShipPool = world.GetPool<IsTransportShipComponent>();
                _isTransportShipPool.Add(entity);

                var loadDataByNameComponent = world.GetPool<LoadDataByNameComponent>();
                ref var loadFactoryDataComponent = ref loadDataByNameComponent.Add(entity);
                loadFactoryDataComponent.AddressableName = AssetsNamesConstants.TRANSPORT_SHIP_DATA_NAME;
            }
        }
    }
}