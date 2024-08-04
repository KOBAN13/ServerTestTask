using UiScripts;
using Zenject;

namespace DI
{
    public class ButtonModelInject : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ButtonSceneModel>().AsSingle().NonLazy();
        }
    }
}