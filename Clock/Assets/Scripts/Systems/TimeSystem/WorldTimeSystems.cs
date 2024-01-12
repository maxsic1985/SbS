using Leopotam.EcsLite;

namespace MSuhinin.Clock
{
    public sealed class WorldTimeSystems
    {
        public WorldTimeSystems(EcsSystems systems)
        {
            systems
                .Add(new WorldTimeInitSystem())
                .Add(new WorldTimeLoadSystem())
                .Add(new LocalTimeSystemUpdate());
        }
    }
}