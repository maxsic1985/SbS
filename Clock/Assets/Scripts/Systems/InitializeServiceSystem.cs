using Leopotam.EcsLite;
using LeopotamGroup.Globals;
using UnityEngine;


namespace MSuhinin.Clock
{
    public sealed class InitializeServiceSystem : IEcsInitSystem
    {
        public InitializeServiceSystem()
        {
           
        }

        public void Init(IEcsSystems systems)
        {
            Service<ITimeService>.Set(new UnityTimeService());
            Service<IWorldTimeService>.Set(new NtpTimeService());
            Service<GameObjectAssetLoader>.Set(new GameObjectAssetLoader());
            Service<ScriptableObjectAssetLoader>.Set(new ScriptableObjectAssetLoader());
            Service<RegexService>.Set(new RegexService());
        }
    }
}