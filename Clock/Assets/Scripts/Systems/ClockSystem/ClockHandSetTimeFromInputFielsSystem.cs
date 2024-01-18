using System;
using Leopotam.EcsLite;
using UnityEngine;
using LeopotamGroup.Globals;

namespace MSuhinin.Clock
{
    public sealed class ClockHandSetTimeFromInputFielsSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filterChechBoxOn;
        private EcsFilter _filterChechBoxOff;
        private EcsFilter _timeFilter;
        private EcsPool<SetTimeFromTextInputComponent> _isHandSetTimeComponent;
        private EcsPool<ClockViewComponent> _clockViewComponentPool;
        private EcsPool<TimeComponent> _timeComponentPool;
        private RegexService _regexService;


        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _filterChechBoxOn = world
                .Filter<ClockViewComponent>()
                .Exc<SetTimeFromTextInputComponent>()
                .End();

            _filterChechBoxOff = world
                .Filter<ClockViewComponent>()
                .Inc<SetTimeFromTextInputComponent>()
                .End();

            _timeFilter = world
                .Filter<TimeComponent>()
                .End();

            _clockViewComponentPool = world.GetPool<ClockViewComponent>();
            _isHandSetTimeComponent = world.GetPool<SetTimeFromTextInputComponent>();
            _timeComponentPool = world.GetPool<TimeComponent>();

            _regexService = Service<RegexService>.Get();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filterChechBoxOn)
            {
                ref var clockViewComponentPool = ref _clockViewComponentPool.Get(entity);
                if (clockViewComponentPool.CheckBoxSetTimeFromTextInput.isOn)
                {
                    clockViewComponentPool.InputFieldTime.interactable = true;
                    ref var _setTimeComponent = ref _isHandSetTimeComponent.Add(entity);
                }
            }

            foreach (var entity in _filterChechBoxOff)
            {
                ref var clockViewComponentPool = ref _clockViewComponentPool.Get(entity);

                if (!clockViewComponentPool.CheckBoxSetTimeFromTextInput.isOn)
                {
                    foreach (var timeEntity in _timeFilter)
                    {
                        var checkInputTime = _regexService.CheckRegex(GameConstants.TIME_PATTERN
                            , clockViewComponentPool.InputFieldTime.text);
                        ref var time = ref _timeComponentPool.Get(timeEntity);

                        var inputText = clockViewComponentPool.InputFieldTime.text;
                        if (checkInputTime && inputText.Length == 5)
                        {
                            time.HOUR = Int32.Parse(inputText[0].ToString() + Int32.Parse(inputText[1].ToString()));
                            time.MIN = Int32.Parse(inputText[3].ToString() + Int32.Parse(inputText[4].ToString()));
                        }
                        else
                        {
                            Debug.Log($"Введен неверный формат времени");
                            clockViewComponentPool.InputFieldTime.text = "00:00";
                        }
                    }

                    _isHandSetTimeComponent.Del(entity);
                }
            }
        }
    }
}