using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Zenject;

namespace AddressablesManagement
{
    public class AddressablesSceneManager : IInitializable
    {
        private SceneInstance _sceneManagerPreviously;
        private bool _isPreviouslyScene;
        private AddressablesManagement _addressablesManagement;

        public AddressablesSceneManager(AddressablesManagement addressablesManagement)
        {
            _addressablesManagement = addressablesManagement;
        }

        public async UniTask LoadScene(string keyScene)
        {
            if (_isPreviouslyScene)
            {
                foreach (var action in _addressablesManagement.GetListReleaseObject())
                {
                    action();
                }
                _addressablesManagement.ClearListReleaseObject();

                await Addressables.UnloadSceneAsync(_sceneManagerPreviously);
            }
            _sceneManagerPreviously = await Addressables.LoadSceneAsync(keyScene, LoadSceneMode.Single);
            _isPreviouslyScene = true;
        }

        public async void Initialize()
        {
            await LoadScene("Menu");
        }
    }
}