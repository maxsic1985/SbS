using UnityEngine;
using UnityEngine.AddressableAssets;



namespace MSuhinin.Clock
{
    [CreateAssetMenu(fileName = nameof(ClockBuildData),
        menuName = EditorMenuConstants.CREATE_DATA_MENU_NAME + nameof(ClockBuildData))]
    public class ClockBuildData : ScriptableObject
    {
        [Header("Prefabs:")]
        public AssetReferenceGameObject ClockPrefab;

       [Header("Other:")]
        public ClockType ClockType;
    }
}
