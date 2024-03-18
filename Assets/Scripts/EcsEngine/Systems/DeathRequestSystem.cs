using EcsEngine.Components;
using EcsEngine.Components.Requests;
using EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems 
{
    internal sealed class DeathRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<HealthCurrent>, Exc<DeathRequest, DeadTag, InactiveTag>> _filter;
        private readonly EcsPoolInject<DeathRequest> _deathRequestPool;
        public void Run (IEcsSystems systems)
        {
            var healthPool = _filter.Pools.Inc1;
            foreach (var entity in _filter.Value)
            {
                if (healthPool.Get(entity).Value <= 0)
                {
                    _deathRequestPool.Value.Add(entity);
                }
            }
        }
    }
}