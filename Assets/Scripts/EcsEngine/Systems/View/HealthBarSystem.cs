using EcsEngine.Components.View;
using EcsEngine.Components;
using EcsEngine.Components.Events;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems.View
{
    internal class HealthBarSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DamageEvent>> _filter;
        private readonly EcsPoolInject<HealthBarLine> _healthBarLine;
        private readonly EcsPoolInject<HealthCurrent> _healthCurrentPool;
        private readonly EcsPoolInject<Health> _health;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                if (_health.Value.Has(entity))
                {
                    var h = (float)_healthCurrentPool.Value.Get(entity).Value / _health.Value.Get(entity).Value;

                    if(h <= 0)
                    {
                        h = 0;
                    }

                    _healthBarLine.Value.Get(entity).Value.localScale = new Vector3(h, 1, 1);
                }
            }
        }
    }
}
