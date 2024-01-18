using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class Test : MonoBehaviour {
    [SerializeField] Text datetimeText;

    void Update ( ) {
        if ( Input.GetMouseButtonUp ( 0 ) && WorldTimeAPI.Instance.IsTimeLodaed ) {
            DateTime currentDateTime = WorldTimeAPI.Instance.GetCurrentDateTime ( );

            datetimeText.text = currentDateTime.ToString ( );
        }
    }
}