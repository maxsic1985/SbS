using UnityEngine;
using UnityEngine.AddressableAssets;


namespace MSuhinin.Clock
{
    [CreateAssetMenu(fileName = nameof(UIMainMenuData),
        menuName = EditorMenuConstants.CREATE_DATA_MENU_NAME + nameof(UIMainMenuData))]
    public class UIMainMenuData : ScriptableObject
    {
        [Header("Menu")] 
        public AssetReferenceGameObject Menu; 

    }
}