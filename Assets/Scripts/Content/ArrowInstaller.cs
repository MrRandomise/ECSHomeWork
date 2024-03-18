using EcsEngine.Components;
using EcsEngine.Components.Tags;
using EcsEngine.Components.View;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Content
{
    public class ArrowInstaller : EntityInstaller
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _speed;
        [SerializeField] private float _lifeTime = 3f;
        protected override void Install(Entity entity)
        {
            var tr = transform;
            var forward = tr.forward;
            entity.AddData(new WeaponTag());
            entity.AddData(new Position{ Value = tr.position});
            entity.AddData(new MoveDirection{ Value = forward});
            entity.AddData(new AttackDamage{ Value = _damage});
            entity.AddData(new MoveSpeed{ Value = _speed});
            entity.AddData(new TransformView{ Value = transform});
            entity.AddData(new TargetPosition{ Value = forward});
            entity.AddData(new Team());
            entity.AddData(new LifeTime{ Value = _lifeTime});
            entity.AddData(new LifeTimeCurrent{ Value = _lifeTime});
        }

        protected override void Dispose(Entity entity)
        {
            
        }
    }
}
