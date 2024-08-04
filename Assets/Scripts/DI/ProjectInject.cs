using AddressablesManagement;
using Zenject;

namespace DI
{
    public class ProjectInject : MonoInstaller
    {
        public override void InstallBindings()
        {
            AddressablesBind();
        }
        
        private void AddressablesBind()
        {
            Container.BindInterfacesAndSelfTo<AddressablesManagement.AddressablesManagement>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Loader.Loader>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AddressablesSceneManager>().AsSingle().NonLazy();
        }
    }
}