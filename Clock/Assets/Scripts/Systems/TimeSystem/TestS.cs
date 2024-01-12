using Leopotam.EcsLite;
using UnityEngine;

namespace MSuhinin.Clock
{
    public class TestS : IEcsRunSystem, IEcsInitSystem
    {
        private EcsPool<IsNeсesaryUpdateTimeFromNet> _isNessesaryUpdateTimeFromNetComponentPool;
        private EcsFilter _filter;
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _filter = _world
                .Filter<IsWorldTimeComponent>()
                .Inc<WorldTimeComponent>()
                .End();
            
            _isNessesaryUpdateTimeFromNetComponentPool = _world.GetPool<IsNeсesaryUpdateTimeFromNet>();

        }

        public void Run(IEcsSystems systems)
        {


            foreach (var entity in _filter)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    Debug.Log("Z");
                    ref var n = ref _isNessesaryUpdateTimeFromNetComponentPool.Add(entity);
                }
            }
        }
    }
}