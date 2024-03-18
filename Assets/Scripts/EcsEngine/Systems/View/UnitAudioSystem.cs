using EcsEngine.Components.Events;
using EcsEngine.Components.Tags;
using EcsEngine.Components.View;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems.View 
{
    internal sealed class UnitAudioSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<UnitTag, UnitAudioView>, Exc<InactiveTag>> _filter;
        private readonly EcsPoolInject<AttackEvent> _attackEventPool;
        private readonly EcsPoolInject<DeathEvent> _deathEventPool;

        public void Run (IEcsSystems systems) 
        {
            foreach (var entity in _filter.Value)
            {
                if (_deathEventPool.Value.Has(entity))
                {
                    _filter.Pools.Inc2.Get(entity).Value.OnDeath();
                    continue;
                }

                if (_attackEventPool.Value.Has(entity))
                {
                    _filter.Pools.Inc2.Get(entity).Value.OnAttack();
                }
            }
        }
    }
}