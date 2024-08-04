using System;
using System.Text.RegularExpressions;
using Zenject;

namespace SaveSystem
{
    public class DataContextLocal
    {
        protected GameData GameData = new();
        private string pattern = @"^(\d{2}):(\d{2}):(\d{2})$";

        public int Score
        {
            get => GameData.score;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException();
                GameData.score = value;
            }
        }

        public string Time
        {
            get => GameData.time;
            set
            {
                if (Regex.IsMatch(value, pattern) == false) throw new FormatException();
                GameData.time = value;
            }
        }

    }
}