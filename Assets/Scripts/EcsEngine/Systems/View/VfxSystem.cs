using EcsEngine.Components.Events;
using EcsEngine.Components.Tags;
using EcsEngine.Components.View;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems.View 
{
    internal sealed class VfxSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<VfxView>, Exc<InactiveTag>> _filter;
        private readonly EcsPoolInject<DamageEvent> _damageEventPool;

        public void Run (IEcsSystems systems) 
        {
            foreach (var entity in _filter.Value)
            {
                if (_damageEventPool.Value.Has(entity))
                {
                    _filter.Pools.Inc1.Get(entity).Value.OnDamage();
                }
            }
        }
    }
}