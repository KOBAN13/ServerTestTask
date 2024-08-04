using SaveSystem;
using UnityEngine;
using Zenject;

namespace DI
{
    public class FirstGameInject : MonoInstaller
    {
        [field: SerializeField] private GameView _gameView;
        public override void InstallBindings()
        {
            BindData();
            BindFirstGame();
        }

        private void BindData()
        {
            Container.Bind<JsonDataContextLocal>().AsSingle().NonLazy();
        }

        private void BindFirstGame()
        {
            Container.BindInterfacesAndSelfTo<GameView>().FromInstance(_gameView).AsSingle().NonLazy();
        }
    }
}