using Leopotam.EcsLite;
using UnityEngine;

namespace MSuhinin.Clock
{
    public static class Extensions
    {
        public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            t = Mathf.Clamp01(t);
            float oneMinusT = 1 - t;

            return
                oneMinusT * oneMinusT * oneMinusT * p0 +
                3f * oneMinusT * oneMinusT * t * p1 +
                3f * oneMinusT * t * t * p2 +
                t * t * t * p3;
        }

        public static void AddPool<T>(IEcsSystems ecsSystem, int entity) where T : struct
        {
            if (ecsSystem.GetWorld().GetPool<T>().Has(entity))
            {
               return;
            }
            else
            {
                ref var component = ref ecsSystem
                    .GetWorld()
                    .GetPool<T>().Add(entity);
            }
        }
    }
}