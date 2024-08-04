using System.Collections.Generic;
using Character;
using Interfases;
using UiScripts.UiSecondGame;
using UnityEngine;
using Zenject;

namespace Loader
{
    public class SpawnerResources : MonoBehaviour
    {
        private AddressablesManagement.AddressablesManagement _addressablesManagement;
        private DiContainer _diContainer;
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private ReferenceLoadAssetSecondGame _assetReference;

        [Inject]
        private void Construct(AddressablesManagement.AddressablesManagement addressablesManagement, DiContainer diContainer)
        {
            _addressablesManagement = addressablesManagement;
            _diContainer = diContainer;
        }
        
        private async void Awake()
        {
            _addressablesManagement.InitAfterLoadResources += OnFindAllScriptsToInject;
            await _addressablesManagement.LoadPrefabsWithLabel(_assetReference.Prefabs);
            
            var list = new List<IInitAfterLoadResources>()
            {
                _diContainer.Resolve<UiGameModel>(),
                _diContainer.Resolve<Movement>(),
                _diContainer.Resolve<Finish>(),
                _diContainer.Resolve<UiGameView>()
            };
            
            list[0].Init(_diContainer.Resolve<UiGameView>());
            list[1].Init(_diContainer.Resolve<PlayerComponents>());
            list[2].Init(_diContainer.Resolve<IPlayerFinish>());
            list[3].Init(_diContainer.Resolve<UiGameModel>());
        }


        private void OnFindAllScriptsToInject()
        {
            _diContainer.BindInterfacesAndSelfTo<UiGameView>().FromInstance(FindObjectOfType<UiGameView>())
                .AsTransient().NonLazy();
            _diContainer.BindInterfacesAndSelfTo<Finish>().FromInstance(FindObjectOfType<Finish>()).AsTransient()
                .NonLazy();
            _diContainer.BindInterfacesAndSelfTo<PlayerComponents>().FromInstance(FindObjectOfType<PlayerComponents>())
                .AsTransient().NonLazy();
        }
    }
}