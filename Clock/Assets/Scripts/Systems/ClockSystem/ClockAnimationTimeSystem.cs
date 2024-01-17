using Leopotam.EcsLite;
using UnityEngine;
using DG.Tweening;

namespace MSuhinin.Clock
{
    public sealed class ClockAnimationTimeSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filterClock;
        private EcsFilter _filterWorldTime;
        private EcsPool<TimeComponent> _worldTimeComponentPool;
        private EcsPool<ClockViewComponent> _clockViewComponentPool;


        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filterClock = world.Filter<IsClockComponent>()
                .Inc<ClockViewComponent>()
                .Exc<SetTimeFromClockHandComponent>()
                .End();
            _filterWorldTime = world.Filter<TimeComponent>().End();

            _worldTimeComponentPool = world.GetPool<TimeComponent>();
            _clockViewComponentPool = world.GetPool<ClockViewComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity1 in _filterClock)
            {
                ref var clockView = ref _clockViewComponentPool.Get(entity1);
                foreach (var entity2 in _filterWorldTime)
                {
                    ref var timeComponentPool = ref _worldTimeComponentPool.Get(entity2);
                    var hour = Mathf.Floor(timeComponentPool.HOUR * GameConstants.HOURS_TO_DEGREES);
                    var min = Mathf.Floor(timeComponentPool.MIN * GameConstants.MINUTES_TO_DEGREES);
                    var sec = Mathf.Floor(timeComponentPool.SEC * GameConstants.MINUTES_TO_DEGREES);

               //  Debug.Log(hour);
                    clockView.HoursEuler.DORotateQuaternion(Quaternion.Euler(0, 0, -hour), GameConstants.TIC_DURATION);
                    clockView.MinutesEuler.DORotateQuaternion(Quaternion.Euler(0, 0, -min), 1);
                    clockView.SecondsEuler.DORotateQuaternion(Quaternion.Euler(0, 0, -sec), 1);
                    clockView.TextTime.text = (timeComponentPool.HOUR+
                                               ":" + timeComponentPool.MIN.ToString("00")+
                                               ":"+timeComponentPool.SEC.ToString("00"));
                    
                }
            }
        }
    }
}