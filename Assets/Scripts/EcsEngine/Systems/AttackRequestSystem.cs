using EcsEngine.Components;
using EcsEngine.Components.Events;
using EcsEngine.Components.Requests;
using EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems 
{
    internal sealed class AttackRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ReadyForAttackTag, AttackCurrentTimeout>, Exc<InactiveTag, AttackRequest, AttackEvent, DeadTag>> _filter;
        private readonly EcsPoolInject<AttackRequest> _attackRequestPool;
        public void Run (IEcsSystems systems)
        {
            var timeoutPool = _filter.Pools.Inc2;
            foreach (var entity in _filter.Value)
            {
                if (timeoutPool.Get(entity).Value == 0)
                {
                    _attackRequestPool.Value.Add(entity);
                }
            }
        }
    }
}