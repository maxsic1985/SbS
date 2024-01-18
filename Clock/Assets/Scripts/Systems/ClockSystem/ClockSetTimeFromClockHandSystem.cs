using Leopotam.EcsLite;
using UnityEngine;


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
        private GameInputSharedData _inputSharedData;

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

            _inputSharedData = systems.GetShared<SharedData>().GetInputSharedData;


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

            if (_inputSharedData.ISForwardMouseDirection)
            {
                if (_curHourAngle <= GameConstants.HALF_ROUND && _hourAngle <= GameConstants.HALF_ROUND)
                {
                    var addh = (_curHourAngle - _hourAngle) / GameConstants.HOURS_TO_DEGREES;
                    time.HOUR = time.HOUR + addh;
                }
                else if (_curHourAngle <= GameConstants.HALF_ROUND && _hourAngle >= GameConstants.HALF_ROUND)
                {
                    var addh = ((GameConstants.ROUND - _hourAngle) + _curHourAngle) / GameConstants.HOURS_TO_DEGREES;
                    time.HOUR = time.HOUR + addh;
                }
                else if (_curHourAngle > GameConstants.HALF_ROUND && _hourAngle >= GameConstants.HALF_ROUND)
                {
                    if (_curHourAngle >= _hourAngle)
                    {
                        var addh = (_curHourAngle - _hourAngle) / GameConstants.HOURS_TO_DEGREES;
                        time.HOUR = time.HOUR + addh;
                    }
                }
                else if (_curHourAngle >= GameConstants.HALF_ROUND && _hourAngle < GameConstants.HALF_ROUND)
                {
                    var addh = (_curHourAngle - _hourAngle) / GameConstants.HOURS_TO_DEGREES;
                    time.HOUR = time.HOUR + addh;
                }
            }
            else
            {
                if (_curHourAngle <= GameConstants.HALF_ROUND && _hourAngle < GameConstants.HALF_ROUND)
                {
                    var addh = (_hourAngle - _curHourAngle) / GameConstants.HOURS_TO_DEGREES;
                    time.HOUR = time.HOUR - addh;
                }
                else if (_curHourAngle <= GameConstants.HALF_ROUND && _hourAngle >= GameConstants.HALF_ROUND)
                {
                    if (_curHourAngle < _hourAngle)
                    {
                        var addh = (_hourAngle - _curHourAngle) / GameConstants.HOURS_TO_DEGREES;
                        time.HOUR = time.HOUR - addh;
                    }
                }
                else if (_curHourAngle >= GameConstants.HALF_ROUND && _hourAngle >= GameConstants.HALF_ROUND)
                {
                    var addh = (_hourAngle - _curHourAngle) / GameConstants.HOURS_TO_DEGREES;
                    time.HOUR = time.HOUR - addh;
                }
                else if (_curHourAngle >= GameConstants.HALF_ROUND && _hourAngle <= GameConstants.HALF_ROUND)
                {
                    var addh = ((GameConstants.ROUND - _curHourAngle) + _hourAngle) / GameConstants.HOURS_TO_DEGREES;
                    time.HOUR = time.HOUR - addh;
                }
            }

            if (time.HOUR >= GameConstants.HOUR_DURATION)
            {
                time.HOUR = time.HOUR >= GameConstants.HOUR_DURATION
                    ? time.HOUR - GameConstants.HOUR_DURATION
                    : time.HOUR;
            }

            if (time.HOUR <= 0)
            {
                time.HOUR = time.HOUR < 0 ? time.HOUR + GameConstants.HOUR_DURATION : time.HOUR;
            }
        }

        private void SetMinutesValue(ClockViewComponent clockViewComponentPool, ref TimeComponent time)
        {
            _minAngle = Mathf.RoundToInt(clockViewComponentPool.MinutesEuler.localRotation.eulerAngles.z);

            if (_inputSharedData.ISForwardMouseDirection)
            {
                var addmin = (_curMinAngle - _minAngle) / GameConstants.MINUTES_TO_DEGREES;
                time.MIN = time.MIN + addmin;
            }
            else
            {
                var addmin = (_minAngle - _curMinAngle) / GameConstants.MINUTES_TO_DEGREES;
                time.MIN = time.MIN - addmin;
            }

            if (time.MIN >= GameConstants.HOUR_DURATION_MIN)
            {
                time.MIN = time.MIN >= GameConstants.HOUR_DURATION_MIN
                    ? time.MIN - GameConstants.HOUR_DURATION_MIN
                    : time.MIN;
            }

            if (time.MIN < 0)
            {
                time.MIN = time.MIN <0 ? time.MIN + GameConstants.HOUR_DURATION_MIN : time.MIN;
            }
        }
    }
}