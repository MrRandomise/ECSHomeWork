using EcsEngine.Components;
using EcsEngine.Components.Tags;
using EcsEngine.Components.View;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Content
{
    public class UnitsPoolInstaller : EntityInstaller
    {
        [SerializeField] private Transform _container;
        [SerializeField] private Transform _containerUnactive;
        protected override void Install(Entity entity)
        {
            entity.AddData(new UnitPoolContainerTag());
            entity.AddData(new Container{ Value = _container });
            entity.AddData(new ContainerUnactive{ Value = _containerUnactive });
            entity.AddData(new Position{ Value = _container.transform.position});
        }

        protected override void Dispose(Entity entity)
        {
            
        }
    }
}
