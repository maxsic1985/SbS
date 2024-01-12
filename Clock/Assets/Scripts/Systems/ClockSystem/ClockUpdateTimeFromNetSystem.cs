using Leopotam.EcsLite;
using UnityEngine;


namespace MSuhinin.Clock
{
    public sealed class ClockUpdateTimeFromNetSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filterClock;
        private EcsFilter _filterWorldTime;
        private EcsPool<WorldTimeComponent> _worldTimeComponentPool;
        private EcsPool<ClockViewComponent> _clockViewComponentPool;
        const float hoursToDegrees = 360 / 12, minutesToDegrees = 360 / 60;


        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            
            _filterClock = world.Filter<IsClockComponent>()
                .Inc<ClockViewComponent>()
                .End();
            _filterWorldTime = world.Filter<WorldTimeComponent>().End();
            
            _worldTimeComponentPool = world.GetPool<WorldTimeComponent>();
            _clockViewComponentPool = world.GetPool<ClockViewComponent>();
      
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity1 in _filterClock)
            {
                ref var clockView = ref _clockViewComponentPool.Get(entity1);
                foreach (var entity2 in _filterWorldTime)
                {
                 
                    ref var worldTimeComponentPool = ref _worldTimeComponentPool.Get(entity2);
                    var hour = Mathf.Floor(worldTimeComponentPool.DateTime.Hour * hoursToDegrees);
                    var min = Mathf.Floor(worldTimeComponentPool.DateTime.Minute * minutesToDegrees);
                    clockView.HoursEuler.rotation=Quaternion.Euler(0,0, -hour);
                    clockView.MinutesEuler.rotation=Quaternion.Euler(0,0, -min);
                    Debug.Log("herer");
                }
            }
        }
    }
}