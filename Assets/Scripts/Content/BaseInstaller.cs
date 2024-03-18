using EcsEngine.Components;
using EcsEngine.Components.Tags;
using EcsEngine.Components.Units;
using EcsEngine.Components.View;
using EcsEngine.Views;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Content
{
    public class BaseInstaller : EntityInstaller
    {
        [SerializeField] private int _team;
        [SerializeField] private float _spawnTimeout;
        [SerializeField] private GameObject _basePrefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private int _health;
        [SerializeField] private Transform _healtBarLine;
        [SerializeField] private float _deathTimeout;
        [SerializeField] private BaseAudioController _baseAudioController;
        [SerializeField] private VfxController _vfxController;
            
        protected override void Install(Entity entity)
        {
            entity.AddData(new BaseTag());
            entity.AddData(new BasePrefab { Value = _basePrefab }); 
            entity.AddData(new Team{ Value = _team});
            entity.AddData(new Health{ Value = _health});
            entity.AddData(new HealthCurrent{ Value = _health});
            entity.AddData(new HealthBarLine { Value = _healtBarLine });
            entity.AddData(new SpawnTimeout{ Value = _spawnTimeout});
            entity.AddData(new SpawnTimeoutCurrent{ Value = _spawnTimeout});
            entity.AddData(new UnitPrefab());
            entity.AddData(new SpawnPoint{ Value = _spawnPoint});
            entity.AddData(new Position{ Value = transform.position});
            entity.AddData(new TransformView{ Value = transform});
            entity.AddData(new BaseAudioView{ Value = _baseAudioController});
            entity.AddData(new VfxView{ Value = _vfxController});
            entity.AddData(new DeathTimeout{ Value = _deathTimeout});
            entity.AddData(new DeathCurrentTimeout{ Value = _deathTimeout});
            entity.AddData(new EndGame { Value = false });
        }

        protected override void Dispose(Entity entity)
        {
            
        }
    }
}
