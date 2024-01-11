using Leopotam.EcsLite;


namespace MSuhinin.Clock
{
    internal class LoadResoursesSystems
    {
        public LoadResoursesSystems(EcsSystems systems)
        {
            systems.Add(new LoadPrefabSystem());
            systems.Add(new LoadDataByNameSystem());
        }
    }
}