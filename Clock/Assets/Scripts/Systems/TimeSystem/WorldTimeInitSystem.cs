using Leopotam.EcsLite;

namespace MSuhinin.Clock
{
    public sealed class WorldTimeInitSystem : IEcsInitSystem
    {
        private EcsPool<IsWorldTimeComponent> _isWorldTimeComponentPool;
        private EcsPool<WorldTimeComponent> _worldTimeComponentPool;
        private EcsPool<IsNeсesaryUpdateTimeFromNet> _isNessesaryUpdateTimeFromNetComponentPool;
        private EcsPool<TimeComponent> _timeComponentPool;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var entity = world.NewEntity();

            _isWorldTimeComponentPool = world.GetPool<IsWorldTimeComponent>();
            _isWorldTimeComponentPool.Add(entity);
            _worldTimeComponentPool = world.GetPool<WorldTimeComponent>();
            _worldTimeComponentPool.Add(entity);
            _isNessesaryUpdateTimeFromNetComponentPool = world.GetPool<IsNeсesaryUpdateTimeFromNet>();
            _isNessesaryUpdateTimeFromNetComponentPool.Add(entity);
            _timeComponentPool = world.GetPool<TimeComponent>();
            _timeComponentPool.Add(entity);
        }
    }
}