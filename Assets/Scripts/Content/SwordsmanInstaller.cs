using Leopotam.EcsLite.Entities;
using EcsEngine.Components;
using EcsEngine.Components.Tags;
using EcsEngine.Components.View;
using EcsEngine.Views;
using UnityEngine;

namespace Content
{
    public sealed class SwordsmanInstaller : EntityInstaller
    {
        [SerializeField] private int _team;
        [SerializeField] private Material _material;
        [SerializeField] private Renderer _body;
        [SerializeField] private float _attackDistance;
        [SerializeField] private float _attackTimeout;
        [SerializeField] private float _attackActionTimeout;
        [SerializeField] private float _speed;
        [SerializeField] private int _health;
        [SerializeField] private Transform _healtBarLine;
        [SerializeField] private int _damage;
        [SerializeField] private float _deathTimeout;
        [SerializeField] private Animator _animator;
        [SerializeField] private UnitsAudioController _unitsAudioController;
        [SerializeField] private VfxController _vfxController;

        protected override void Install(Entity entity)
        {
            var position = transform.position;
            entity.AddData(new UnitTag());
            entity.AddData(new Position{ Value = position});
            entity.AddData(new TargetPosition{ Value = position});
            entity.AddData(new MoveDirection{ Value = Vector3.zero});
            entity.AddData(new MoveSpeed{ Value = _speed});
            entity.AddData(new Rotation{ Value = transform.rotation});
            entity.AddData(new TransformView{ Value = transform});
            entity.AddData(new Team{ Value = _team });
            entity.AddData(new TeamColor { Value = _material });
            entity.AddData(new BodyColor { Value = _body });
            entity.AddData(new AttackDistance{ Value = _attackDistance});
            entity.AddData(new AttackTimeOut{ Value = _attackTimeout});
            entity.AddData(new AttackActionTimeout{ Value = _attackActionTimeout});
            entity.AddData(new AttackCurrentTimeout());
            entity.AddData(new Health{ Value = _health});
            entity.AddData(new HealthBarLine { Value = _healtBarLine });
            entity.AddData(new AttackDamage{ Value = _damage});
            entity.AddData(new TargetEntity());
            entity.AddData(new DeathTimeout{ Value = _deathTimeout});
            entity.AddData(new DeathCurrentTimeout());
            entity.AddData(new AnimatorView{ Value = _animator});
            entity.AddData(new UnitAudioView{ Value = _unitsAudioController});
            entity.AddData(new VfxView{ Value = _vfxController});
            entity.AddData(new PreviousPosition{ Value = position});
            entity.AddData(new HealthCurrent{ Value = _health});
        }

        protected override void Dispose(Entity entity)
        {
            
        }
    }
}
