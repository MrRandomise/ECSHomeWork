using EcsEngine.Components;
using EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems 
{
    internal sealed class AttackTimeoutSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackCurrentTimeout>, Exc<InactiveTag, DeadTag>> _filter;
        public void Run (IEcsSystems systems)
        {
            var attackTimeoutPool = _filter.Pools.Inc1;
            var deltaTime = Time.deltaTime;
            foreach (var entity in _filter.Value)
            {
                ref var timeout = ref attackTimeoutPool.Get(entity);
                timeout.Value = Mathf.Max(0, timeout.Value - deltaTime);
            }
        }
    }
}