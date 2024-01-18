using Leopotam.EcsLite;


namespace MSuhinin.Clock
{
    internal class ServicesSystems
    {
        public ServicesSystems(EcsSystems systems)
        {
            systems
                .Add(new InitializeServiceSystem());
        }
    }
}