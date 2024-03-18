using EcsEngine.Components;
using EcsEngine.Components.Events;
using EcsEngine.Components.Requests;
using EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems 
{
    internal sealed class AttackStartAttackSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackRequest, AttackTimeOut , AttackCurrentTimeout>> _filter;
        private readonly EcsPoolInject<StartAttackEvent> _startAttackPool;
        private readonly EcsPoolInject<AttackingTag> _attackingPool;
        public void Run (IEcsSystems systems)
        {
            var attackRequestPool = _filter.Pools.Inc1;
            var attackTimeoutPool = _filter.Pools.Inc2;
            var attackTCurrentTimeoutPool = _filter.Pools.Inc3;
            foreach (var entity in _filter.Value)
            {
                attackRequestPool.Del(entity);
                _startAttackPool.Value.Add(entity);
                _attackingPool.Value.Add(entity);
                attackTCurrentTimeoutPool.Get(entity).Value = attackTimeoutPool.Get(entity).Value;
            }
        }
    }
}