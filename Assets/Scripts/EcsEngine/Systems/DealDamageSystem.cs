using EcsEngine.Components;
using EcsEngine.Components.Events;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems 
{
    internal sealed class DealDamageSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DamageEvent>> _filter;
        private readonly EcsPoolInject<HealthCurrent> _healthCurrentPool;
        public void Run (IEcsSystems systems)
        {
            var damageEventPool = _filter.Pools.Inc1;
            foreach (var entity in _filter.Value)
            {
                if (_healthCurrentPool.Value.Has(entity))
                {
                    _healthCurrentPool.Value.Get(entity).Value -= damageEventPool.Get(entity).Value;
                }
            }
        }
    }
}