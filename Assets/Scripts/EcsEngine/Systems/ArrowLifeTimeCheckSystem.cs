using EcsEngine.Components;
using EcsEngine.Components.Requests;
using EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems 
{
    internal sealed class ArrowLifeTimeCheckSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<WeaponTag, LifeTimeCurrent>, Exc<InactiveTag, ArrowRemoveRequest>> _filter;
        private readonly EcsPoolInject<ArrowRemoveRequest> _requestPool;
        public void Run (IEcsSystems systems)
        {
            var deltaTime = Time.deltaTime;
            foreach (var entity in _filter.Value)
            {
                ref var timeout = ref _filter.Pools.Inc2.Get(entity);
                timeout.Value = timeout.Value - deltaTime;
                if (timeout.Value < 0)
                {
                    _requestPool.Value.Add(entity);
                }
            }
        }
    }
}