using System;
using System.Text.RegularExpressions;
using SaveSystem;
using Zenject;

namespace UiScripts.UiSecondGame
{
    public class BestTime : IInitializable, IDisposable, IPlayerFinish
    {
        private JsonDataContextLocal _jsonDataContextLocal;
        private ITime _time;
        private UiGameModel _uiGameModel;
        private event Action PlayerFinish; 

        public BestTime(JsonDataContextLocal jsonDataContextLocal, ITime time, UiGameModel uiGameModel)
        {
            _jsonDataContextLocal = jsonDataContextLocal;
            _time = time;
            _uiGameModel = uiGameModel;
        }

        private async void PlayerFinishShowResult()
        {
            await _jsonDataContextLocal.Load();

            _jsonDataContextLocal.Time = "10:10:10";

            if (ConvertStringToDateTime(_time.TimeStartAfterGame.Value) < ConvertStringToDateTime(_jsonDataContextLocal.Time))
            {
                _jsonDataContextLocal.Time = _time.TimeStartAfterGame.Value;
                await _jsonDataContextLocal.Save();
            }
            _uiGameModel.ShowBestTime(_jsonDataContextLocal.Time);
            _uiGameModel.ShowCanvas();
        }

        private DateTime ConvertStringToDateTime(string time)
        {
            if (Regex.IsMatch(time, @"^(\d{2}):(\d{2}):(\d{2})$") == false) throw new FormatException();
            var split = time.Split(':');
            return new DateTime(1, 1, 1, int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2]));
        }
        
        public void OnPlayerFinish()
        {
            PlayerFinish?.Invoke();
        }
        

        public void Initialize()
        {
            PlayerFinish += PlayerFinishShowResult;
        }

        public void Dispose()
        {
            PlayerFinish -= PlayerFinishShowResult;
        }
    }

    public interface IPlayerFinish
    {
        void OnPlayerFinish();
    }
}