using Leopotam.EcsLite;

namespace MSuhinin.Clock
{
    public sealed class WebTimeSystems
    {
        public WebTimeSystems(EcsSystems systems)
        {
            systems
                .Add(new ClockLagSystem())
                .Add(new WebTimeInitSystem())
                .Add(new WebUpLoadSystem())
                .Add(new LocalTimeUpdateSystem());
        }
    }
}