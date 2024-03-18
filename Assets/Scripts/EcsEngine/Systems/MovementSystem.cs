using EcsEngine.Components;
using EcsEngine.Components.Events;
using EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    internal sealed class MovementSystem : IEcsRunSystem
    {
        
        private readonly EcsFilterInject<Inc<MoveDirection, MoveSpeed, Position>, Exc<ReadyForAttackTag, StartAttackEvent, InactiveTag, DeadTag>> _filter;
        public void Run (IEcsSystems systems)
        {
            var moveDirectionPool = _filter.Pools.Inc1;
            var moveSpeedPool = _filter.Pools.Inc2;
            var positionPool = _filter.Pools.Inc3;
            

            var deltaTime = Time.deltaTime;

            foreach (var entity in _filter.Value)
            {
                ref var position = ref positionPool.Get(entity);
                ref var moveDirection = ref moveDirectionPool.Get(entity);
                ref var moveSpeed = ref moveSpeedPool.Get(entity);
                position.Value += moveDirection.Value * (moveSpeed.Value * deltaTime);
            }
        }
    }
}