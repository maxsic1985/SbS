using Leopotam.EcsLite;
using LeopotamGroup.Globals;


namespace MSuhinin.Clock
{
    public sealed class InitializeServiceSystem : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            Service<IWorldTimeService>.Set(new NtpTimeService());
            Service<GameObjectAssetLoader>.Set(new GameObjectAssetLoader());
            Service<ScriptableObjectAssetLoader>.Set(new ScriptableObjectAssetLoader());
            Service<RegexService>.Set(new RegexService());
        }
    }
}