using System;
using System.IO;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace SaveSystem
{
    public class JsonDataContextServer : DataContextServer
    {
        private const string PathServer = "Json/JsonServer.json";
        private const string PathLocal = "GameData/JsonServer.json";

        public async UniTask Load()
        {
            var json = await FtpServerDownload.DownloadJson(PathServer);
            GameDataServerCurrent = JsonConvert.DeserializeObject<GameDataServer>(json);
            Paths = GameDataServerCurrent.Paths;
        }

        public async UniTask Save()
        {
            try
            {
                var json = JsonConvert.SerializeObject(GameDataServerCurrent);
                await using var writer = new StreamWriter(FilePath);
                await writer.WriteAsync(json);
                writer.Close();
                await Upload.UploadJson(FilePath, PathServer);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"{e.Message}");
            }
        }
        
        private string FilePath => $"{Application.persistentDataPath}/{PathLocal}";
    }
}