using Leopotam.EcsLite;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace MSuhinin.Clock
{
    public sealed class ClockHandSetTimeSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filterChechBoxOn;
        private EcsFilter _filterChechBoxOff;
        private EcsFilter _timeFilter;
        private EcsPool<IsHandSetTimeComponent> _isHandSetTimeComponent;
        private EcsPool<ClockViewComponent> _clockViewComponentPool;
        private EcsPool<TimeComponent> _timeComponentPool;
        private int curAngle;
        private int angle;
        private Vector3 cur;
        private GameSharedData _sharedData;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _filterChechBoxOn = world
                .Filter<ClockViewComponent>()
                .Exc<IsHandSetTimeComponent>()
                .End();

            _filterChechBoxOff = world
                .Filter<ClockViewComponent>()
                .Inc<IsHandSetTimeComponent>()
                .End();

            _timeFilter = world
                .Filter<TimeComponent>()
                .End();

            _sharedData = systems.GetShared<SharedData>().GetSharedData;


            _clockViewComponentPool = world.GetPool<ClockViewComponent>();
            _isHandSetTimeComponent = world.GetPool<IsHandSetTimeComponent>();
            _timeComponentPool = world.GetPool<TimeComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filterChechBoxOn)
            {
                ref var clockViewComponentPool = ref _clockViewComponentPool.Get(entity);

                if (clockViewComponentPool.CheckBoxSetTime.isOn)
                {
                    ref var _setTimeComponent = ref _isHandSetTimeComponent.Add(entity);

                    curAngle = Mathf.RoundToInt(clockViewComponentPool
                        .HoursEuler.transform.rotation.eulerAngles.z);
                    angle = curAngle;
                    Debug.Log("Curangle" + curAngle);
                    Debug.Log("angle" + angle);
                    cur = clockViewComponentPool
                        .HoursEuler.transform.rotation.eulerAngles;
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
                        //  Debug.Log(clockViewComponentPool.HoursEuler.transform.rotation.eulerAngles.z);


                        angle = Mathf.RoundToInt(clockViewComponentPool.HoursEuler.localRotation.eulerAngles.z);
                        // var angle = Mathf.RoundToInt(clockViewComponentPool.HoursEuler.rotation.eulerAngles.z);

                        if (angle == curAngle)
                        {
                            return;
                        }

                        Debug.Log("Direction" + _sharedData.GetMouseDirection);
                        var summ = curAngle + angle;


                        if (_sharedData.GetMouseDirection)
                        {
                            if (curAngle < 180 && angle < 180)
                            {
                                var addh = (curAngle - angle) / 30;
                                time.HOUR = time.HOUR + addh;
                             
                            }
                            else if (curAngle < 180 && angle > 180)
                            {
                                var addh = ((360 - angle) + curAngle) / 30;
                                time.HOUR = time.HOUR + addh;
                          
                            }
                            else if (curAngle > 180 && angle > 180)
                            {
                                if (curAngle > angle)
                                {
                                    var addh = (curAngle - angle) / 30;
                                    time.HOUR = time.HOUR + addh;
                                
                                }
                            }
                            else if (curAngle > 180 && angle < 180)
                            {
                                var addh = (curAngle - angle) / 30;
                                time.HOUR = time.HOUR + addh;
                            
                            }
                        }
                        else
                        {
                            if (curAngle < 180 && angle < 180)
                            {
                                var addh = (angle - curAngle) / 30;
                                time.HOUR = time.HOUR - addh;
                              
                            }
                            else if (curAngle < 180 && angle > 180)
                            {
                                if (curAngle < angle)
                                {
                                    var addh = (angle - curAngle) / 30;
                                    time.HOUR = time.HOUR - addh;
                              
                                }
                            }
                            else if (curAngle > 180 && angle > 180)
                            {
                                var addh = (angle - curAngle) / 30;
                                time.HOUR = time.HOUR - addh;
                            }
                            else if (curAngle > 180 && angle < 180)
                            {
                                var addh = ((360 - curAngle) + angle) / 30;
                                time.HOUR = time.HOUR - addh;
                            }
                        }

               
                        if (time.HOUR > 23)
                        {
                            time.HOUR = time.HOUR > 23 ? time.HOUR - 23 : time.HOUR;
                        }
                        
                        if (time.HOUR < 0)
                        {
                            time.HOUR = time.HOUR < 0 ? time.HOUR + 23 : time.HOUR;
                        }

                        //1 берем угол стрелки
                        //2 прибавляем или убавляем если угол уменьшается то прибавляем
//тек время - или + угол/30 
//тек угол -новый угол = 30= 1 час 
                    }

                    _isHandSetTimeComponent.Del(entity);
                }
            }
        }
    }
}