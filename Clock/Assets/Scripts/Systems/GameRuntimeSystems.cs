using Leopotam.EcsLite;

namespace MSuhinin.Clock
{
    public class GameRuntimeSystems
    {
        public GameRuntimeSystems(EcsSystems systems)
        {
            new ClockSystems(systems);
        }
    }
}