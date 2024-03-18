using EcsEngine.Components;
using EcsEngine.Components.Events;
using EcsEngine.Components.Tags;
using EcsEngine.Components.Units;
using EcsEngine.Components.View;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems.View
{
    internal sealed class DestroyBaseSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DeathEvent>> _filter;
        private readonly EcsPoolInject<VfxView> _fvx;
        private readonly EcsPoolInject<BasePrefab> _basePrefab;
        private readonly EcsPoolInject<BaseTag> _baseTag;
        private readonly EcsFilterInject<Inc<EndGame>> _endGame;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                if (_baseTag.Value.Has(entity))
                {
                    _basePrefab.Value.Get(entity).Value.gameObject.SetActive(false);
                    _fvx.Value.Get(entity).Value.OnDamageParticleStop();
                    _fvx.Value.Get(entity).Value.OnDestr();

                    foreach (var endGame in _endGame.Value)
                    {
                        _endGame.Pools.Inc1.Get(endGame).Value = true;
                    }
                }
            }
        }
    }
}