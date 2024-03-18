using EcsEngine.Components;
using EcsEngine.Components.Events;
using EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using EcsEngine.Components.View;

namespace EcsEngine.Systems.View
{
    internal sealed class UIGameOverSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DeathEvent, BaseTag, Team>> _filter;
        private readonly EcsFilterInject<Inc<UiPaneViewl, WinnerTextView>> _panel;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                if (_filter.Pools.Inc2.Has(entity))
                {
                    foreach (var panel in _panel.Value)
                    {
                        _panel.Pools.Inc1.Get(panel).Value.SetActive(true);
                        var textVictory = "";
                        if (_filter.Pools.Inc3.Get(entity).Value == 0)
                        {
                            textVictory = $"Победила команда <color=#ff0000>красных</color> ";
                        }
                        else
                        {
                            textVictory = $"Победила команда <color=#0000ff>синих</color> ";
                        }
                        _panel.Pools.Inc2.Get(panel).Value.text = textVictory;
                    }
                }
            }
        }
    }
}
