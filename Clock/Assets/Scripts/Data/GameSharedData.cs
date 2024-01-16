using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


namespace MSuhinin.Clock
{
    [CreateAssetMenu(fileName = nameof(GameSharedData),
        menuName = EditorMenuConstants.CREATE_DATA_MENU_NAME + nameof(GameSharedData), order = 2)]
    public sealed class GameSharedData : ScriptableObject
    {
        [FormerlySerializedAs("_direction")] [SerializeField] private bool mouseDirection;

        //  public bool GetDirection => _direction;
        public bool GetMouseDirection
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