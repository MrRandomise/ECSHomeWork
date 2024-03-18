using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    internal sealed class UnityTeamColor : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Team, TeamColor, BodyColor>> _filter;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var team = _filter.Pools.Inc1.Get(entity).Value;
                var teamColor = _filter.Pools.Inc2.Get(entity).Value;
                var body = _filter.Pools.Inc3.Get(entity).Value;

                if (team == 0)
                {
                    body.material = teamColor;
                }
            }
        }
    }
}
