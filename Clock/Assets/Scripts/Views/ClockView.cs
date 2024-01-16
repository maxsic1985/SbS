using System;
using System.Collections;
using System.Collections.Generic;
using MSuhinin.Clock;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClockView : BaseView
{
    [SerializeField] private Transform _minutesTransform;
    [SerializeField] private Transform _hoursTransform;
    [SerializeField] private Transform _secondsTransform;
    [SerializeField] private Text _textTime;
    [SerializeField] private Toggle _checkBoxSetTime;

    public Transform MinutesTransform => _minutesTransform;
    public Transform HoursTransform => _hoursTransform;
    public Transform SecondsTransform => _secondsTransform;
    public Text TextTime => _textTime;
    public Toggle CheckBoxSetTime => _checkBoxSetTime;
    
}