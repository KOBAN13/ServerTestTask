using Character;
using SaveSystem;
using UiScripts.UiSecondGame;
using Zenject;
using Timer = UiScripts.UiSecondGame.Timer;

namespace DI
{
    public class SecondGameInject : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInput();
            BindPlayer();
            BindSaveSystem();
            BindTimers();
            BindUi();
        }

        private void BindUi()
        {
            Container.BindInterfacesAndSelfTo<UiGameModel>().AsSingle().NonLazy();
        }

        private void BindSaveSystem()
        {
            Container.BindInterfacesAndSelfTo<JsonDataContextLocal>().AsSingle().NonLazy();
        }

        private void BindTimers()
        {
            Container.BindInterfacesAndSelfTo<Timer>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BestTime>().AsSingle().NonLazy();
        }

        private void BindPlayer()
        {
            Container.BindInterfacesAndSelfTo<Movement>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Player>().AsSingle().NonLazy();
        }

        private void BindInput()
        {
            Container.BindInterfacesAndSelfTo<InputSystem>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<InputSystemPC>().AsSingle().NonLazy();
        }
    }
}