using EcsEngine.Components.Events;
using EcsEngine.Components.Tags;
using EcsEngine.Components.Units;
using EcsEngine.Components.View;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems.View 
{
    internal sealed class BaseAudioSystem : IEcsRunSystem 
    {
        private readonly EcsFilterInject<Inc<BaseTag, BaseAudioView>, Exc<InactiveTag>> _filter;
        private readonly EcsPoolInject<DamageEvent> _damageEventPool;
        private readonly EcsPoolInject<DeathEvent> _deathEventPool;
        


        public void Run (IEcsSystems systems) 
        {
            foreach (var entity in _filter.Value)
            {
                if (_deathEventPool.Value.Has(entity) )
                {
                    _filter.Pools.Inc2.Get(entity).Value.WhenDestroy();
                    _filter.Pools.Inc2.Get(entity).Value.OnVictory();
                    continue;
                }
                if (_damageEventPool.Value.Has(entity))
                {
                    _filter.Pools.Inc2.Get(entity).Value.OnDamage();
                }
            }
        }
    }
}