using System;
using EcsEngine.Components;
using EcsEngine.Components.Events;
using EcsEngine.Components.Requests;
using EcsEngine.Components.Tags;
using EcsEngine.Components.Units;
using EcsEngine.Components.View;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;

namespace EcsEngine.Systems
{
    internal sealed class UnitsSpawnSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<UnitSpawnRequest, SpawnPoint, UnitPrefab, Team, SpawnTimeout, SpawnTimeoutCurrent>> _filter;
        private readonly EcsFilterInject<Inc<UnitPoolContainerTag, Container>> _poolFilter;
        private readonly EcsCustomInject<EntityManager> _entityManager;
        private readonly EcsPoolInject<SpawnUnitEvent> _spawnUnitEventPool;

        public void Run(IEcsSystems systems)
        {
            var poolEntity = -1;
            foreach (var entity in _poolFilter.Value)
            {
                poolEntity = entity;
            }

            if (poolEntity == -1)
            {
                throw new Exception("No unit pool container found!");
            }

            foreach (var entity in _filter.Value)
            {
                ref var timeout = ref _filter.Pools.Inc6.Get(entity);
                if (timeout.Value > 0)
                    continue;
                var unit = _filter.Pools.Inc3.Get(entity);
                var newUnit = _entityManager.Value.Create(unit.Value, _filter.Pools.Inc2.Get(entity).Value.position,
                    unit.Value.transform.rotation, _poolFilter.Pools.Inc2.Get(poolEntity).Value);

                newUnit.GetData<Team>().Value = _filter.Pools.Inc4.Get(entity).Value;

                _filter.Pools.Inc1.Del(entity);
                timeout.Value = _filter.Pools.Inc5.Get(entity).Value;

                _spawnUnitEventPool.Value.Add(entity);
            }
        }
    }
}