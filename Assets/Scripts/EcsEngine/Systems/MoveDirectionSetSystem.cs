using EcsEngine.Components;
using EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems 
{
    internal sealed class MoveDirectionSetSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Position, TargetPosition, MoveDirection>, Exc<InactiveTag>> _filter;
        public void Run (IEcsSystems systems)
        {
            var positionPool = _filter.Pools.Inc1;
            var targetPositionPool = _filter.Pools.Inc2;
            var moveDirectionPool = _filter.Pools.Inc3;
            foreach (var entity in _filter.Value)
            {
                ref var moveDirection = ref moveDirectionPool.Get(entity);
                moveDirection.Value = (targetPositionPool.Get(entity).Value - positionPool.Get(entity).Value).normalized;
                moveDirection.Value.y = 0;
            }
        }
    }
}