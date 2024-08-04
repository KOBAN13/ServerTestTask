using System.IO;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace SaveSystem
{
    public class JsonDataContextLocal : DataContextLocal
    {
        private const string Path = "GameData\\GameData";

        public async UniTask Load()
        {
            if (!File.Exists(FilePath)) return;
            using var reader = new StreamReader(FilePath);
            var json = await reader.ReadToEndAsync();
            GameData = JsonUtility.FromJson<GameData>(json);
        }

        public async UniTask Save()
        {
            var json = JsonConvert.SerializeObject(GameData);
            await using var writer = new StreamWriter(FilePath);
            await writer.WriteAsync(json);
        }
        
        private string FilePath => $"{Application.persistentDataPath}/{Path}.json";
    }
}