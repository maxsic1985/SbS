using Leopotam.EcsLite;

namespace MSuhinin.Clock
{
    public sealed class WorldTimeSystems
    {
        public WorldTimeSystems(EcsSystems systems)
        {
            systems
                .Add(new ClockLagSystem())
                .Add(new WorldTimeInitSystem())
                .Add(new WorldTimeUpLoadWEBSystem())
                .Add(new LocalTimeSystemUpdate());
        }
    }
}