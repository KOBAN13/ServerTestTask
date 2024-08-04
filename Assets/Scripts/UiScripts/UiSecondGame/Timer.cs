using System.Text;
using UniRx;
using UnityEngine;
using Zenject;

namespace UiScripts.UiSecondGame
{
    public class Timer : ITickable, ITime
    {
        private float _currentTime;
        private StringBuilder _stringBuilder = new();
        public ReactiveProperty<string> TimeStartAfterGame { get; } = new();

        public void Tick()
        {
            _currentTime += Time.deltaTime;
            TimeStartAfterGame.Value = FormatTime(_currentTime);
        }

        private string FormatTime(float time)
        {
            var hours = Mathf.FloorToInt(time / 3600);
            var minutes = Mathf.FloorToInt((time % 3600) / 60);
            var seconds = Mathf.FloorToInt(time % 60);

            _stringBuilder.Clear();
            _stringBuilder.Append($"{hours:00}:{minutes:00}:{seconds:00}");
            return _stringBuilder.ToString();
        }
    }
    
    public interface ITime
    {
        ReactiveProperty<string> TimeStartAfterGame { get; }
    }
}