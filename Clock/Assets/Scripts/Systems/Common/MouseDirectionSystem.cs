using Leopotam.EcsLite;
using Leopotam.EcsLite.Unity.Ugui;
using LeopotamGroup.Globals;
using UnityEngine;
using UnityEngine.Scripting;

namespace MSuhinin.Clock
{
    public class MouseDirectionSystem:IEcsInitSystem, IEcsRunSystem

    {
    private EcsFilter _filter;
    private EcsPool<MouseDirectionComponent> _mouseInputPool;
    
    
    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();
        _filter = world.Filter<MouseDirectionComponent>()
            .End();

        _mouseInputPool = world.GetPool<MouseDirectionComponent>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            ref var direction = ref _mouseInputPool.Get(entity);
            // if ((direction.Position - direction.LastPosition).normalized.x > 0)
            // {
            //     direction.IsPositive = true;
            // }
            // else
            // {
            //     direction.IsPositive = false;
            // }
            Debug.Log(direction.IsPositive);
        }
    }
    }
}