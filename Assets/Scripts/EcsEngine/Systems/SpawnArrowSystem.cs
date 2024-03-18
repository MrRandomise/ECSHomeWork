using System;
using EcsEngine.Components;
using EcsEngine.Components.Requests;
using EcsEngine.Components.Tags;
using EcsEngine.Components.View;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace EcsEngine.Systems 
{
    internal sealed class SpawnArrowSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<FireRequest, ArrowWeapon>, Exc<InactiveTag, DeadTag>> _filter;
        private readonly EcsFilterInject<Inc<ArrowPoolContainerTag, Container>> _poolFilter;
        private readonly EcsCustomInject<EntityManager> _entityManager;
        private readonly EcsPoolInject<TargetPosition> _targetPositionPool;
        private readonly EcsPoolInject<Team> _teamPool;

        public void Run (IEcsSystems systems) 
        {
            var weaponPool = _filter.Pools.Inc2;
            var requestPool = _filter.Pools.Inc1;
            var poolEntity = -1;
            foreach (var entity in _poolFilter.Value)
            {
                poolEntity = entity;
            }

            if (poolEntity == -1)
            {
                throw new Exception("No arrow pool container found!");
            }
            foreach (var entity in _filter.Value)
            {
                var arrow = weaponPool.Get(entity);
                var position = arrow._point.position;
                var newArrow =_entityManager.Value.Create(arrow._arrow, position,
                    Quaternion.Euler(Vector3.left), _poolFilter.Pools.Inc2.Get(poolEntity).Value);
                _targetPositionPool.Value.Get(newArrow.Id).Value = _targetPositionPool.Value.Get(entity).Value;
                _teamPool.Value.Get(newArrow.Id).Value = _teamPool.Value.Get(entity).Value;
                requestPool.Del(entity);
            }
        }
    }
}