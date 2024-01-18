using System;
using System.Text.RegularExpressions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.UI;

namespace MSuhinin.Clock
{
    public sealed class ClockSetTimeFromClockHandSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filterChechBoxOn;
        private EcsFilter _filterChechBoxOff;
        private EcsFilter _timeFilter;
        private EcsPool<SetTimeFromClockHandComponent> _isHandSetTimeComponent;
        private EcsPool<ClockViewComponent> _clockViewComponentPool;
        private EcsPool<TimeComponent> _timeComponentPool;
        private int _curHourAngle;
        private int _hourAngle;
        private int _curMinAngle;
        private int _minAngle;
        private Vector3 cur;
        private GameSharedData _sharedData;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _filterChechBoxOn = world
                .Filter<ClockViewComponent>()
                .Exc<SetTimeFromClockHandComponent>()
                .End();

            _filterChechBoxOff = world
                .Filter<ClockViewComponent>()
                .Inc<SetTimeFromClockHandComponent>()
                .End();

            _timeFilter = world
                .Filter<TimeComponent>()
                .End();

            _sharedData = systems.GetShared<SharedData>().GetSharedData;


            _clockViewComponentPool = world.GetPool<ClockViewComponent>();
            _isHandSetTimeComponent = world.GetPool<SetTimeFromClockHandComponent>();
            _timeComponentPool = world.GetPool<TimeComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filterChechBoxOn)
            {
                ref var clockViewComponentPool = ref _clockViewComponentPool.Get(entity);
             
                if (clockViewComponentPool.CheckBoxSetTimeFromClockHand.isOn)
                {
                    ref var _setTimeComponent = ref _isHandSetTimeComponent.Add(entity);

                    _curHourAngle =
                        Mathf.RoundToInt(clockViewComponentPool.HoursEuler.transform.rotation.eulerAngles.z);
                    _hourAngle = _curHourAngle;

                    _curMinAngle =
                        Mathf.RoundToInt(clockViewComponentPool.MinutesEuler.transform.rotation.eulerAngles.z);
                    _minAngle = _curMinAngle;
                }
            }

            foreach (var entity in _filterChechBoxOff)
            {
                ref var clockViewComponentPool = ref _clockViewComponentPool.Get(entity);

                if (!clockViewComponentPool.CheckBoxSetTimeFromClockHand.isOn)
                {
                    foreach (var timeEntity in _timeFilter)
                    {
                        ref var time = ref _timeComponentPool.Get(timeEntity);

                       
                        SetHoursValue(clockViewComponentPool, ref time);
                        SetMinutesValue(clockViewComponentPool, ref time);
                    }

                    _isHandSetTimeComponent.Del(entity);
                }
            }
        }

        private void SetHoursValue(ClockViewComponent clockViewComponentPool, ref TimeComponent time)
        {
            _hourAngle = Mathf.RoundToInt(clockViewComponentPool.HoursEuler.localRotation.eulerAngles.z);

            if (_hourAngle == _curHourAngle)
            {
                return;
            }

            if (_sharedData.GetMouseDirection)
            {
                if (_curHourAngle <= 180 && _hourAngle <= 180)
                {
                    var addh = (_curHourAngle - _hourAngle) / 30;
                    time.HOUR = time.HOUR + addh;
                }
                else if (_curHourAngle <= 180 && _hourAngle >= 180)
                {
                    var addh = ((360 - _hourAngle) + _curHourAngle) / 30;
                    time.HOUR = time.HOUR + addh;
                }
                else if (_curHourAngle > 180 && _hourAngle >= 180)
                {
                    if (_curHourAngle >= _hourAngle)
                    {
                        var addh = (_curHourAngle - _hourAngle) / 30;
                        time.HOUR = time.HOUR + addh;
                    }
                }
                else if (_curHourAngle >= 180 && _hourAngle < 180)
                {
                    var addh = (_curHourAngle - _hourAngle) / 30;
                    time.HOUR = time.HOUR + addh;
                }
            }
            else
            {
                if (_curHourAngle <=180 && _hourAngle < 180)
                {
                    var addh = (_hourAngle - _curHourAngle) / 30;
                    time.HOUR = time.HOUR - addh;
                }
                else if (_curHourAngle <= 180 && _hourAngle >= 180)
                {
                    if (_curHourAngle < _hourAngle)
                    {
                        var addh = (_hourAngle - _curHourAngle) / 30;
                        time.HOUR = time.HOUR - addh;
                    }
                }
                else if (_curHourAngle >= 180 && _hourAngle >= 180)
                {
                    var addh = (_hourAngle - _curHourAngle) / 30;
                    time.HOUR = time.HOUR - addh;
                }
                else if (_curHourAngle >= 180 && _hourAngle <= 180)
                {
                    var addh = ((360 - _curHourAngle) + _hourAngle) / 30;
                    time.HOUR = time.HOUR - addh;
                }
            }

            if (time.HOUR >= 24)
            {
                time.HOUR = time.HOUR >= 24 ? time.HOUR - 24 : time.HOUR;
            }

            if (time.HOUR <= 0)
            {
                time.HOUR = time.HOUR < 0 ? time.HOUR + 24 : time.HOUR;
            }
        }

        private void SetMinutesValue(ClockViewComponent clockViewComponentPool, ref TimeComponent time)
        {
            _minAngle = Mathf.RoundToInt(clockViewComponentPool.MinutesEuler.localRotation.eulerAngles.z);

            if (_minAngle == _curMinAngle)
            {
                //   return;
            }

            if (_sharedData.GetMouseDirection)
            {
                var addmin = (_curMinAngle - _minAngle) / 6;
                time.MIN = time.MIN + addmin;
            }
            else
            {
                var addmin = (_minAngle - _curMinAngle) / 6;
                time.MIN = time.MIN - addmin;
            }

            if (time.MIN > 59)
            {
                time.MIN = time.MIN > 59 ? time.MIN - 59 : time.MIN;
            }

            if (time.MIN < 1)
            {
                time.MIN = time.MIN < 1 ? time.MIN + 59 : time.MIN;
            }
        }
    }
}