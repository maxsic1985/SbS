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
    public sealed class ClockDigitalHandSetTimeSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filterChechBoxOn;
        private EcsFilter _filterChechBoxOff;
        private EcsFilter _timeFilter;
        private EcsPool<SetTimeFromClockHandComponent> _isHandSetTimeComponent;
        private EcsPool<ClockViewComponent> _clockViewComponentPool;
        private EcsPool<TimeComponent> _timeComponentPool;


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

            _clockViewComponentPool = world.GetPool<ClockViewComponent>();
            _isHandSetTimeComponent = world.GetPool<SetTimeFromClockHandComponent>();
            _timeComponentPool = world.GetPool<TimeComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filterChechBoxOn)
            {
                ref var clockViewComponentPool = ref _clockViewComponentPool.Get(entity);
                if (clockViewComponentPool.CheckBoxSetTime.isOn)
                {
                    clockViewComponentPool.InputFieldTime.interactable = true;
                    ref var _setTimeComponent = ref _isHandSetTimeComponent.Add(entity);
                }
            }

            foreach (var entity in _filterChechBoxOff)
            {
                ref var clockViewComponentPool = ref _clockViewComponentPool.Get(entity);

                if (!clockViewComponentPool.CheckBoxSetTime.isOn)
                {
                    foreach (var timeEntity in _timeFilter)
                    {
                        ref var time = ref _timeComponentPool.Get(timeEntity);
                        var c = new Regex(@"^(([0-1]?[0-9])|([2][0-3]))(:([0-5][0-9])){1,2}$").IsMatch(
                            clockViewComponentPool.InputFieldTime.text);
                        if (c)
                        {
                            //use stringBuilder
                            time.HOUR = Int32.Parse(clockViewComponentPool.InputFieldTime.text[0].ToString()
                                                    + Int32.Parse(clockViewComponentPool.InputFieldTime.text[1]
                                                        .ToString()));
                            time.MIN = Int32.Parse(clockViewComponentPool.InputFieldTime.text[3].ToString()
                                                   + Int32.Parse(clockViewComponentPool.InputFieldTime.text[4]
                                                       .ToString()));
                        }
                    }

                    _isHandSetTimeComponent.Del(entity);
                }
            }
        }
    }
}