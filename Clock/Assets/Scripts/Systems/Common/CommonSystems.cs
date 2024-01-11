using Leopotam.EcsLite;



namespace MSuhinin.Clock
{
    internal class CommonSystems
    {
        public CommonSystems(EcsSystems systems)
        {
            systems.Add(new TransformMovingSystem());
            systems.Add(new SynchronizeTransformAndPositionSystem());
            systems.Add(new TimerRunSystem());
        }
    }
}