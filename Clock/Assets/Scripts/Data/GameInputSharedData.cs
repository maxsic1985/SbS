using UnityEngine;


namespace MSuhinin.Clock
{
    [CreateAssetMenu(fileName = nameof(GameInputSharedData),
        menuName = EditorMenuConstants.CREATE_DATA_MENU_NAME + nameof(GameInputSharedData), order = 2)]
    public sealed class GameInputSharedData : ScriptableObject
    {
        
       [SerializeField] private bool mouseDirection;
       
        public bool ISForwardMouseDirection
        {
            get
            {
                return mouseDirection;
            }
            set
            {
                mouseDirection = value;
            }
        }
    }
}