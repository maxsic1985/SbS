using Leopotam.EcsLite;

namespace MSuhinin.Clock
{
    public class InitializeAllSystem
    {
        public InitializeAllSystem(EcsSystems systems)
        {
            new ServicesSystems(systems);
            new LoadResoursesSystems(systems);
            new WebTimeSystems(systems);
            new ClockSystems(systems);
        }
    }
}