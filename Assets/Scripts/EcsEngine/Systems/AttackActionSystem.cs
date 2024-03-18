using EcsEngine.Components;
using EcsEngine.Components.Events;
using EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems 
{
    internal sealed class AttackActionSystem : IEcsRunSystem
    {
        private readonly
            EcsFilterInject<Inc<AttackingTag, AttackActionTimeout, AttackCurrentTimeout>, Exc<InactiveTag, AttackEvent, DeadTag>> _filter;

        private readonly EcsPoolInject<AttackEvent> _attackEventPool;
        private readonly EcsPoolInject<TargetEntity> _targetEntityPool;
        private readonly EcsPoolInject<AttackDamage> _damagePool;
        private readonly EcsPoolInject<DamageEvent> _damageEventPool;
        public void Run (IEcsSystems systems)
        {
            var attackingTagPool = _filter.Pools.Inc1;
            var attackActionTimeoutPool = _filter.Pools.Inc2;
            var attackTimeoutPool = _filter.Pools.Inc3;
            foreach (var entity in _filter.Value)
            {
                if (attackTimeoutPool.Get(entity).Value < attackActionTimeoutPool.Get(entity).Value)
                {
                    _attackEventPool.Value.Add(entity);
                    var damage = _damagePool.Value.Get(entity);
                    var enemy = _targetEntityPool.Value.Get(entity).Value;
                    if (!_damageEventPool.Value.Has(enemy))
                    {
                        _damageEventPool.Value.Add(enemy).Value = damage.Value;
                    }
                    else
                    {
                        _damageEventPool.Value.Get(enemy).Value += damage.Value;
                    }
                    attackingTagPool.Del(entity);
                }
            }
        }
    }
}