using UiScripts;
using UiScripts.Canvases;
using UnityEngine;
using Zenject;

namespace DI
{
    public class UserInterfaceInject : MonoInstaller
    {
        [field: SerializeField] private CanvasView _canvasView;
        public override void InstallBindings()
        {
            BindButtons();
            BindCanvas();
        }

        private void BindButtons()
        {
            Container.BindInterfacesAndSelfTo<ButtonModel>().AsSingle().NonLazy();
        }

        private void BindCanvas()
        {
            Container.BindInterfacesAndSelfTo<CanvasModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CanvasView>().FromInstance(_canvasView).AsSingle().NonLazy();
        }
    }
}