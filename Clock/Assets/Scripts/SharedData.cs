using System.Threading.Tasks;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


namespace  MSuhinin.Clock
{
    public sealed class SharedData
    {
        private GameInputSharedData _playerInputShared;
        public GameInputSharedData GetInputSharedData => _playerInputShared;

        public async Task Init()
        {
            AsyncOperationHandle<GameInputSharedData> handlePlayer =
                Addressables.LoadAssetAsync<GameInputSharedData>(AssetsNamesConstants.GAME_SHARED_DATA);
            await handlePlayer.Task;
            
            _playerInputShared = handlePlayer.Result;

        }
    }
}