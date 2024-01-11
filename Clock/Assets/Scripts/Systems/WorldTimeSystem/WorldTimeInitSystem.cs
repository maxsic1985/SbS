using Leopotam.EcsLite;
using LeopotamGroup.Globals;
using UnityEngine;

namespace MSuhinin.Clock
{
    public class WorldTimeInitSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private IWorldTimeService wts;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<IsCameraComponent>().End();
            wts = Service<IWorldTimeService>.Get();

        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
              Debug.Log(wts.GetCurrentDateTime().ToString());
              
            }
        }
    }
}