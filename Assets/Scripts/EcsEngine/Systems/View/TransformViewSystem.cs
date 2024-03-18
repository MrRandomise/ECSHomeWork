using EcsEngine.Components;
using EcsEngine.Components.View;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems.View
{
    internal sealed class TransformViewSystem : IEcsPostRunSystem
    {
        private readonly EcsFilterInject<Inc<TransformView, Position, TargetPosition>> _filter;
        private readonly EcsPoolInject<Rotation> _rotationPool;
        public void PostRun(IEcsSystems systems)
        {
            var rotationPool = _rotationPool.Value;
            foreach (var entity in _filter.Value)
            {
                ref var transform = ref _filter.Pools.Inc1.Get(entity);
                ref var position = ref _filter.Pools.Inc2.Get(entity);
                transform.Value.position = position.Value;
                var lookTarget = _filter.Pools.Inc3.Get(entity).Value;
                transform.Value.LookAt(new Vector3(lookTarget.x, transform.Value.position.y, lookTarget.z));

                if (rotationPool.Has(entity))
                {
                    Vector3 dir = lookTarget - transform.Value.position;
                    var rotation = Quaternion.LookRotation(dir, Vector3.up);
                    transform.Value.rotation = rotation;
                }
            }
        }
    }
}