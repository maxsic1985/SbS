using Leopotam.EcsLite;

namespace MSuhinin.Clock
{
    public class GameRuntimeSystems
    {
        public GameRuntimeSystems(EcsSystems systems)
        {
            new WorldTimeSystems(systems);
            new ClockSystems(systems);
        }
    }
}