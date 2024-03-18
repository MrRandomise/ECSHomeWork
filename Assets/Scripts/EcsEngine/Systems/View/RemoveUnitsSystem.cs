using System;
using EcsEngine.Components;
using EcsEngine.Components.Tags;
using EcsEngine.Components.View;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems.View 
{
    internal sealed class RemoveUnitsSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DeadTag>> _filter;
        private readonly EcsFilterInject<Inc<UnitPoolContainerTag, ContainerUnactive>> _unitContainer;
        private readonly EcsPoolInject<Position> _positionPool;
        private readonly EcsPoolInject<TransformView> _transformViewPool;
        private readonly EcsPoolInject<DeathCurrentTimeout> _deathCurrentTimeout;
        private readonly EcsPoolInject<InactiveTag> _inactivePool;

        public void Run (IEcsSystems systems)
        {
            var deltaTime = Time.deltaTime;
            int containerEntity = GetContainerPoolEntity();

            var deadPool = _filter.Pools.Inc1;
            
            foreach (var entity in _filter.Value)
            {
                ref var timeout = ref _deathCurrentTimeout.Value.Get(entity);
                timeout.Value -= deltaTime;
                if (timeout.Value <= 0)
                {
                    _transformViewPool.Value.Get(entity).Value.SetParent(_unitContainer.Pools.Inc2.Get(containerEntity).Value);
                    _positionPool.Value.Get(entity).Value = _positionPool.Value.Get(containerEntity).Value;
                    _inactivePool.Value.Add(entity);
                    deadPool.Del(entity);
                }
            }
        }

        private int GetContainerPoolEntity()
        {
            foreach (var entity in _unitContainer.Value)
            {
                return entity;
            }
            throw new Exception("Не найден контейнер для юнитов!");
        }
    }
}