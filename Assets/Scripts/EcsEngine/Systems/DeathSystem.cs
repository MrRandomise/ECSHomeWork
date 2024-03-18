using EcsEngine.Components;
using EcsEngine.Components.Events;
using EcsEngine.Components.Requests;
using EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems 
{
    internal sealed class DeathSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DeathRequest>> _filter;
        private readonly EcsPoolInject<DeadTag> _deadPool; 
        private readonly EcsPoolInject<DeathTimeout> _deadTimeoutPool; 
        private readonly EcsPoolInject<DeathCurrentTimeout> _deadCurrentTimeoutPool;
        private readonly EcsPoolInject<DeathEvent> _deathEventPool;
        public void Run (IEcsSystems systems)
        {
            var deathRequestPool = _filter.Pools.Inc1;
            foreach (var entity in _filter.Value)
            {
                deathRequestPool.Del(entity);
                _deadPool.Value.Add(entity);
                _deathEventPool.Value.Add(entity);
                _deadCurrentTimeoutPool.Value.Get(entity).Value = _deadTimeoutPool.Value.Get(entity).Value;
            }
        }
    }
}