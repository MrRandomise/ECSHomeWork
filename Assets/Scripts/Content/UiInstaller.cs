using EcsEngine.Components.View;
using Leopotam.EcsLite.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Content
{
    public class UiInstaller : EntityInstaller
    {
        [SerializeField] GameObject _uiPanel;
        [SerializeField] Text _uiText;

        protected override void Install(Entity entity)
        {
            entity.AddData(new UiPaneViewl{ Value = _uiPanel });
            entity.AddData(new WinnerTextView { Value = _uiText });
        }

        protected override void Dispose(Entity entity)
        {

        }
    }
}
