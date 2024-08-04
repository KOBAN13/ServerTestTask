using System;
using SaveSystem;
using UniRx;

namespace UiScripts.UIFirstGame
{
    public class GameModel : IDisposable
    {
        private JsonDataContextLocal _dataContextLocal;
        private readonly GameView _gameView;
        private readonly ButtonSceneModel _buttonSceneModel;
        private ReactiveProperty<int> Score { get; } = new();

        public GameModel(JsonDataContextLocal dataContextLocal, GameView gameView, ButtonSceneModel buttonSceneModel)
        {
            _dataContextLocal = dataContextLocal;
            _gameView = gameView;
            _buttonSceneModel = buttonSceneModel;

            Initialize();
        }

        public async void AddScore()
        {
            _dataContextLocal.Score += 1;
            Score.Value = _dataContextLocal.Score;
            await _dataContextLocal.Save();
        }
        
        public void Dispose()
        {
            Score?.Dispose();
        }

        public void LoadMenu()
        {
            _buttonSceneModel.LoadMenuScene();
        }

        private async void Initialize()
        {
            await _dataContextLocal.Load();
            Score.Subscribe(_ => _gameView.Score.text = $"Score: {_dataContextLocal.Score}");
        }
    }
}