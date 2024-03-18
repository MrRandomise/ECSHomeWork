using EcsEngine.Components;
using EcsEngine.Components.Tags;
using EcsEngine.Components.View;
using EcsEngine.Views;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Content
{
    public class ArcherInstaller : EntityInstaller
    {
        [SerializeField] private int _team;
        [SerializeField] private Material _material;
        [SerializeField] private Renderer _body;
        [SerializeField] private float _attackDistance;
        [SerializeField] private float _attackTimeout;
        [SerializeField] private float _fireTimeout;
        [SerializeField] private Entity _arrowPrefab;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private int _health;
        [SerializeField] private Transform _healtBarLine;
        [SerializeField] private float _speed;
        [SerializeField] private float _deathTimeout;
        [SerializeField] private Animator _animator;
        [SerializeField] private UnitsAudioController _unitsAudioController;
        [SerializeField] private VfxController _vfxController;

        protected override void Install(Entity entity)
        {
            var transformPosition = transform.position;
            entity.AddData(new UnitTag());
            entity.AddData(new Position{ Value = transformPosition});
            entity.AddData(new TargetPosition{ Value = transformPosition});
            entity.AddData(new MoveDirection{ Value = Vector3.zero});
            entity.AddData(new MoveSpeed{ Value = _speed });
            entity.AddData(new Rotation{ Value = transform.rotation});
            entity.AddData(new TransformView{ Value = transform});
            entity.AddData(new Team{ Value = _team});
            entity.AddData(new TeamColor { Value = _material });
            entity.AddData(new BodyColor { Value = _body });
            entity.AddData(new AttackDistance{ Value = _attackDistance});
            entity.AddData(new AttackTimeOut{ Value = _attackTimeout});
            entity.AddData(new AttackFireTimeout{ Value = _fireTimeout});
            entity.AddData(new AttackCurrentTimeout());
            entity.AddData(new Health{ Value = _health});
            entity.AddData(new HealthCurrent{ Value = _health});
            entity.AddData(new HealthBarLine { Value = _healtBarLine });
            entity.AddData(new TargetEntity{ Value = -1});
            entity.AddData(new DeathTimeout{ Value = _deathTimeout});
            entity.AddData(new DeathCurrentTimeout());
            entity.AddData(new AnimatorView{ Value = _animator});
            entity.AddData(new UnitAudioView{ Value = _unitsAudioController});
            entity.AddData(new VfxView{ Value = _vfxController});
            entity.AddData(new PreviousPosition{ Value = transformPosition});
            entity.AddData(new ArrowWeapon
            {
                _arrow = _arrowPrefab,
                _point = _firePoint,
            });
        }

        protected override void Dispose(Entity entity)
        {
            
        }
    }
}
