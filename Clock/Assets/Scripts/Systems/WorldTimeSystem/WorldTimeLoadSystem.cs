﻿using Leopotam.EcsLite;
using LeopotamGroup.Globals;
using UnityEngine;


namespace MSuhinin.Clock
{
    public sealed class WorldTimeLoadSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private IWorldTimeService _worldTimeService;
        private EcsPool<WorldTimeComponent> _worldTimeComponentPool;
        private EcsPool<IsWorldTimeComponent> _isGetWorldTimeComponent;

        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<IsWorldTimeComponent>()
                .Inc<WorldTimeComponent>()
                .End();
            _worldTimeService = Service<IWorldTimeService>.Get();
            _worldTimeComponentPool = _world.GetPool<WorldTimeComponent>();
            _isGetWorldTimeComponent = _world.GetPool<IsWorldTimeComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                ref var worldTimeComponentPool = ref _worldTimeComponentPool.Get(entity);
                worldTimeComponentPool.DateTime = _worldTimeService.GetCurrentDateTime();
                Debug.Log(worldTimeComponentPool.DateTime.ToString());
              _isGetWorldTimeComponent.Del(entity);
            }
        }
    }
}