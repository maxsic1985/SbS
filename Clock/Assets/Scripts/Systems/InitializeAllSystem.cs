using Leopotam.EcsLite;

namespace MSuhinin.Clock
{
    internal class InitializeAllSystem
    {
        public InitializeAllSystem(EcsSystems systems)
        {
            new ServicesSystems(systems);
            new LoadResoursesSystems(systems);
            new CommonSystems(systems);
            new GameRuntimeSystems(systems);
        }
    }


    public class GameRuntimeSystems
    {
        public GameRuntimeSystems(EcsSystems systems)
        {
           // new CameraSystems(systems);
        }
    }
}