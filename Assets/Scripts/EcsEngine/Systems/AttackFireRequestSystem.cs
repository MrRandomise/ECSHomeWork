using EcsEngine.Components;
using EcsEngine.Components.Events;
using EcsEngine.Components.Requests;
using EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems 
{
    internal sealed class AttackFireRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackingTag, AttackFireTimeout, AttackCurrentTimeout>, Exc<InactiveTag, DeadTag>> _filter;
        private readonly EcsPoolInject<AttackEvent> _attackEventPool;
        private readonly EcsPoolInject<FireRequest> _fireRequestPool;
        
        public void Run (IEcsSystems systems) 
        {
            var attackingTagPool = _filter.Pools.Inc1;
            var attackFireTimeoutPool = _filter.Pools.Inc2;
            var attackTimeoutPool = _filter.Pools.Inc3;
            foreach (var entity in _filter.Value)
            {
                if (attackTimeoutPool.Get(entity).Value < attackFireTimeoutPool.Get(entity).Value)
                {
                    _attackEventPool.Value.Add(entity);
                    _fireRequestPool.Value.Add(entity);
                    attackingTagPool.Del(entity);
                }
            }
        }
    }
}