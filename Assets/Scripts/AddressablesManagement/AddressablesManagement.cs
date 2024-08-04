using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace AddressablesManagement
{
    public class AddressablesManagement
    {
        private readonly List<Action> _objectsToRelease = new();
        private readonly Loader.Loader _loader;
        public Action InitAfterLoadResources;

        public AddressablesManagement(Loader.Loader loader)
        {
            _loader = loader;
        }

        public async void LoadPrefabs(List<AssetReferenceT<GameObject>> listPrefab)
        {
            foreach (var reference in listPrefab)
            {
                var prefab = await _loader.LoadResourcesUsingReference(reference);
                var prefabInstantiate = await Addressables.InstantiateAsync(prefab);
                _objectsToRelease.Add(() =>
                {
                    _loader.ClearMemoryInstance(prefabInstantiate);
                    _loader.ClearMemory(prefab.Item2);
                });
                InitAfterLoadResources?.Invoke();
            }
        }
        
        public async UniTask LoadPrefabsWithLabel(AssetLabelReference labelReference)
        {
            var list = await _loader.LoadAllResourcesUseLabel<GameObject>(labelReference);
            foreach (var prefab in list.Item1)
            {
                var prefabInstantiate = await Addressables.InstantiateAsync(prefab.name);
                _objectsToRelease.Add(() =>
                {
                    _loader.ClearMemoryInstance(prefabInstantiate);
                });
            }
            InitAfterLoadResources?.Invoke();
            _loader.ClearMemory(list.Item2);
        }

        public IReadOnlyList<Action> GetListReleaseObject()
        {
            return _objectsToRelease;
        }

        public void ClearListReleaseObject() => _objectsToRelease.Clear();
    }
}