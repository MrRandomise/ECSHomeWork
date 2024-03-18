using System.Collections.Generic;
using EcsEngine.Components;
using EcsEngine.Components.Requests;
using EcsEngine.Components.Units;
using Leopotam.EcsLite.Entities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameSystem
{
    public class SpawnController : MonoBehaviour
    {
        [SerializeField] private Entity _unit;
        [SerializeField] private List<Entity> _unitList;

        [Button]
        private void Spawn()
        {
            if (!_unit.HasData<UnitSpawnRequest>() && !_unit.GetData<EndGame>().Value)
            {
                _unit.GetData<UnitPrefab>().Value = _unitList[Random.Range(0, _unitList.Count)];
                _unit.AddData(new UnitSpawnRequest());  
            }
        }
    }
}
