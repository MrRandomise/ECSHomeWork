using EcsEngine.Components;
using EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems 
{
    internal sealed class TargetSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Team, Position, TargetPosition>, Exc<AttackingTag, InactiveTag, DeadTag, WeaponTag>> _filter;
        private readonly EcsFilterInject<Inc<Team, Position>, Exc<InactiveTag, DeadTag, WeaponTag>> _targetFilter;
        private readonly EcsPoolInject<TargetEntity> _targetEntityPool;
        public void Run (IEcsSystems systems) 
        {
            var teamPool = _filter.Pools.Inc1;
            var positionPool = _filter.Pools.Inc2;
            var targetPositionPool = _filter.Pools.Inc3;
            
            var currentDistance = 0f;
            
            foreach (var entity in _filter.Value)
            {
                var targetFound = false;
                foreach (var enemy in _targetFilter.Value)
                {
                    if(teamPool.Get(enemy).Value == teamPool.Get(entity).Value) continue;
                    if (!targetFound)
                    {
                        targetPositionPool.Get(entity).Value = positionPool.Get(enemy).Value;
                        currentDistance =
                            Vector3.Distance(positionPool.Get(enemy).Value, positionPool.Get(entity).Value);
                        AddEnemyEntityAsTarget(entity, enemy);
                        targetFound = true;
                    }
                    else
                    {
                        var currentEnemy = positionPool.Get(enemy).Value;
                        var entityPosition = positionPool.Get(entity).Value;
                        var distance = Vector3.Distance(currentEnemy, entityPosition);
                        if (!(distance < currentDistance)) continue;
                        targetPositionPool.Get(entity).Value = currentEnemy;
                        AddEnemyEntityAsTarget(entity, enemy);
                        currentDistance = distance;
                    }
                }
                if (!targetFound)
                {
                    targetPositionPool.Get(entity).Value = positionPool.Get(entity).Value;
                    _targetEntityPool.Value.Get(entity).Value = -1;
                }
            }
        }

        private void AddEnemyEntityAsTarget(int entity, int enemy)
        {
            if (_targetEntityPool.Value.Has(entity))
            {
                _targetEntityPool.Value.Get(entity).Value = enemy;
            }
        }
    }
}