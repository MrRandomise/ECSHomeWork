using EcsEngine.Components;
using EcsEngine.Components.Tags;
using EcsEngine.Components.View;
using Leopotam.EcsLite.Entities;
using UnityEngine;


namespace Content
{
    public class ArrowPoolInstaller : EntityInstaller
    {
        [SerializeField] private Transform _containerUnactive;
        [SerializeField] private Transform _container;
        protected override void Install(Entity entity)
        {
            entity.AddData(new ArrowPoolContainerTag());
            entity.AddData(new ContainerUnactive{ Value = _containerUnactive });
            entity.AddData(new Container{ Value = _container });
            entity.AddData(new Position{ Value = _containerUnactive.transform.position});
        }

        protected override void Dispose(Entity entity)
        {
            
        }
    }
}
