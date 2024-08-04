using AddressablesManagement;

namespace UiScripts
{
    public class ButtonSceneModel
    {
        private AddressablesSceneManager _addressablesSceneManager;

        public ButtonSceneModel(AddressablesSceneManager addressablesSceneManager)
        {
            _addressablesSceneManager = addressablesSceneManager;
        }
        
        public async void LoadMenuScene()
        {
            await _addressablesSceneManager.LoadScene("Menu");
        }
    }
}