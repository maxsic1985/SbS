using System.Collections;
using System.Collections.Generic;
using MSuhinin.Clock;
using UnityEngine;

public class ClockView : BaseView
{
    [SerializeField] private Transform _minutesTransform;
    [SerializeField] private Transform _hoursTransform;

    public Transform MinutesTransform => _minutesTransform;
    public Transform HoursTransform => _hoursTransform;
}