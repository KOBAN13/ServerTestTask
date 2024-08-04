using System;
using AddressablesManagement;
using Interfases;
using UniRx;

namespace UiScripts.UiSecondGame
{
    public class UiGameModel : IDisposable, IInitAfterLoadResources
    {
        private UiGameView _uiGameView;
        private ITime _timer;
        private bool _isInit;
        private AddressablesSceneManager _addressablesSceneManager;

        public UiGameModel(ITime timer, AddressablesSceneManager addressablesSceneManager)
        {
            _timer = timer;
            _addressablesSceneManager = addressablesSceneManager;
        }

        public void ShowBestTime(string time) => _uiGameView.TextBestTime.text = time;
        public void ShowCanvas() => _uiGameView.Buttons.enabled = true;
        public async void LoadScene(string scene) => await _addressablesSceneManager.LoadScene(scene);

        public void Initialize()
        {
            _timer.TimeStartAfterGame.Subscribe(_ => _uiGameView.TextCurrentTime.text = _timer.TimeStartAfterGame.Value);
        }

        public void Dispose()
        {
            _timer.TimeStartAfterGame.Dispose();
        }

        public void Init<T>(T mono)
        {
            _uiGameView = mono as UiGameView;
            Initialize();
        }
    }
}