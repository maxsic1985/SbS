﻿using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

namespace MSuhinin.Clock
{
    public class WorldTimeServiceFromApi : IWorldTimeService
    {
        private DateTime _currentDateTime = DateTime.Now;
        private bool IsTimeLodaed = false;

        public void Initialize(string API_URL)
        {
            IEnumerator result = GetRealDateTimeFromAPI( API_URL);
        }

        public DateTime GetCurrentDateTime()
        {
            //here we don't need to get the datetime from the server again
            // just add elapsed time since the game start to _currentDateTime

            return _currentDateTime.AddSeconds(Time.realtimeSinceStartup);
        }

        struct TimeData
        {
            //public string client_ip;
            //...
            public string datetime;
            //..
        }

        private IEnumerator GetRealDateTimeFromAPI(string API_URL)
        {
            UnityWebRequest webRequest = UnityWebRequest.Get(API_URL);
            Debug.Log("getting real datetime...");

            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                //error
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                //success
                TimeData timeData = JsonUtility.FromJson<TimeData>(webRequest.downloadHandler.text);
                //timeData.datetime value is : 2020-08-14T15:54:04+01:00

                _currentDateTime = ParseDateTime(timeData.datetime);
                IsTimeLodaed = true;

                Debug.Log("Success.");
            }
        }

        //datetime format => 2020-08-14T15:54:04+01:00
        DateTime ParseDateTime(string datetime)
        {
            //match 0000-00-00
            string date = Regex.Match(datetime, @"^\d{4}-\d{2}-\d{2}").Value;

            //match 00:00:00
            string time = Regex.Match(datetime, @"\d{2}:\d{2}:\d{2}").Value;

            return DateTime.Parse(string.Format("{0} {1}", date, time));
        }
    }
}