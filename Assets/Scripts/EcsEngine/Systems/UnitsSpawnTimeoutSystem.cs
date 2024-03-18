using EcsEngine.Components;
using EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems 
{
    internal sealed class UnitsSpawnTimeoutSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<SpawnTimeoutCurrent>, Exc<InactiveTag>> _filter;
        public void Run (IEcsSystems systems)
        {
            var deltaTime = Time.deltaTime;
            foreach (var entity in _filter.Value)
            {
                _filter.Pools.Inc1.Get(entity).Value -= deltaTime;
            }
        }
    }
}