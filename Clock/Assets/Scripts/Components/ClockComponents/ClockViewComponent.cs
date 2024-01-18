using UnityEngine;
using UnityEngine.UI;

namespace MSuhinin.Clock
{
    public struct ClockViewComponent
    {
        public Transform HoursEuler;
        public Transform MinutesEuler;
        public Transform SecondsEuler;
        public Text TextTime;
        public Toggle CheckBoxSetTimeFromClockHand;
        public Toggle CheckBoxSetTimeFromTextInput;
        public InputField InputFieldTime;
    
    }
}