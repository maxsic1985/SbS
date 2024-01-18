using MSuhinin.Clock;
using UnityEngine;
using UnityEngine.UI;


public sealed class InputTextView : BaseView
{
    [SerializeField] private InputField _inputField;

    public InputField InputField => _inputField;
    
}