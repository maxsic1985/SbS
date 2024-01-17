using Leopotam.EcsLite;

namespace MSuhinin.Clock
{
    public sealed class ClockSystems
    {
        public ClockSystems(EcsSystems systems)
        {
            systems
                .Add(new ClockInitSystem())
                .Add(new ClockLoadSystem())
                .Add(new ClockBuildSystem())
                .Add(new ClockAnimationTimeSystem())
                .Add(new ClockAnalogHandSetTimeSystem())
                .Add(new ClockDigitalHandSetTimeSystem());
        }
    }
}