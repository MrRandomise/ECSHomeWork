using EcsEngine.Components;
using EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems 
{
    internal sealed class AttackDistanceCheckSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Position, TargetPosition, AttackDistance>, Exc<InactiveTag, DeadTag>> _filter;
        private readonly EcsPoolInject<ReadyForAttackTag> _readyForAttackPool;
        public void Run (IEcsSystems systems)
        {
            var positionPool = _filter.Pools.Inc1;
            var targetPositionPool = _filter.Pools.Inc2;
            var attackDistancePool = _filter.Pools.Inc3;

            foreach (var entity in _filter.Value)
            {
                var distance = Vector3.Distance(positionPool.Get(entity).Value, targetPositionPool.Get(entity).Value);
                var hasTag = _readyForAttackPool.Value.Has(entity);
                if (distance > attackDistancePool.Get(entity).Value || distance == 0f)
                {
                    if (hasTag)
                    {
                        _readyForAttackPool.Value.Del(entity);
                    }
                }
                else
                {
                    if (!hasTag)
                    {
                        _readyForAttackPool.Value.Add(entity);
                    }
                }
            }
        }
    }
}