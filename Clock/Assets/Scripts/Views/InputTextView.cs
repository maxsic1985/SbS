using System;
using System.Collections;
using System.Collections.Generic;
using MSuhinin.Clock;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public sealed class InputTextView : BaseView
{
    [SerializeField] private InputField _inputField;

    public InputField InputField => _inputField;

 
}