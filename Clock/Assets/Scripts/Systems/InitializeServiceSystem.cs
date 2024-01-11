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
            Service<GameObjectAssetLoader>.Set(new GameObjectAssetLoader());
            Service<ScriptableObjectAssetLoader>.Set(new ScriptableObjectAssetLoader());

            if (Application.isEditor)
                Service<IInputService>.Set(new KeyboardInputService());
            else
                Service<IInputService>.Set(new SwipeService());
        }
    }
}