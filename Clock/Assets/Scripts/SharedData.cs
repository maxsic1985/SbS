using System.Threading.Tasks;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


namespace  MSuhinin.Clock
{
    public sealed class SharedData
    {
        private GameSharedData _playerShared;
        public GameSharedData GetSharedData => _playerShared;

        public async Task Init()
        {
            AsyncOperationHandle<GameSharedData> handlePlayer =
                Addressables.LoadAssetAsync<GameSharedData>(AssetsNamesConstants.GAME_SHARED_DATA);
            await handlePlayer.Task;
            
            _playerShared = handlePlayer.Result;

        }
    }
}