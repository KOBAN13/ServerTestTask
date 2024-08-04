using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Interfases;
using SaveSystem;
using ServerScripts;
using UnityEngine;
using Zenject;

public class FTPServerDownload : MonoBehaviour, IDownload
{
    private JsonDataContextServer _jsonDataContextServer;

    [Inject]
    private void Construct(JsonDataContextServer jsonDataContextServer) =>
        _jsonDataContextServer = jsonDataContextServer;

    private async void Awake()
    {
        await DownLoadFilesPaths();
        DownloadFileFromServer(AnyBundle.AnyBundle);
        DownloadFileFromServer(AnyBundle.FirstGame);
        DownloadFileFromServer(AnyBundle.SecondGame);
    }

    public async UniTask DownLoadFilesPaths()
    {
        // Dictionary<AnyBundle, List<FilePaths>> dic = new Dictionary<AnyBundle, List<FilePaths>>
        // {
        //     { AnyBundle.FirstGame , new List<FilePaths>()},
        //     { AnyBundle.SecondGame , new List<FilePaths>()},
        //     { AnyBundle.AnyBundle , new List<FilePaths>()}
        // };
        //
        
        await _jsonDataContextServer.Load();
        
        /*foreach (var keyPair in _jsonDataContextServer.Paths.Keys)
        {
            foreach (var filePaths in _jsonDataContextServer.Paths[keyPair])
            {
                string[] split = filePaths.FilePath.Split("\\");
                
                dic[keyPair].Add(new FilePaths
                {
                    FilePath =  $"{ServerData.FilePathGameLocalLoad}/{split[^1]}",
                    Directory = ServerData.FilePathGameLocalLoad,
                    FileRemotePath = filePaths.FileRemotePath
                });
            }
        }*/
    }

    public void DownloadFileFromServer(AnyBundle bundle)
    {
        CheckExistDirectory(ServerData.FilePathGameLocal);
        _jsonDataContextServer.Paths[bundle].ForEach(async filePath =>
            await DownloadFile(filePath.FileRemotePath, filePath.FilePath));
    }
    
    public async UniTask<string> DownloadJson(string remoteFilePath)
    {
        try
        {
            FtpWebRequest request = ServerData.ConnectionToFtpServer(remoteFilePath, WebRequestMethods.Ftp.DownloadFile);

            UniTask<WebResponse> responseTask = request.GetResponseAsync().AsUniTask();
            FtpWebResponse response = (FtpWebResponse)await responseTask;
            
            await using var stream = response.GetResponseStream();
            using var streamReader = new StreamReader(stream);
            string json = await streamReader.ReadToEndAsync();

            return json;
        }
        catch (Exception e)
        {
            Debug.LogError($"Ошибка {e.Message}");
            throw;
        }
    }
    
    private void CheckExistDirectory(string localPathGame)
    {
        if (!Directory.Exists(localPathGame))
        {
            Directory.CreateDirectory(localPathGame);
        }
    }

    private async UniTask DownloadFile(string remoteFilePath, string filePath)
    {
        try
        {
            FtpWebRequest request = ServerData.ConnectionToFtpServer(remoteFilePath, WebRequestMethods.Ftp.DownloadFile);
            UniTask<WebResponse> response = request.GetResponseAsync().AsUniTask();
            FtpWebResponse responseFtp = (FtpWebResponse)await response;

            await using Stream stream = responseFtp?.GetResponseStream();

            FileStream fileStream = new FileStream($"{filePath}", FileMode.Create);
            if (stream != null) await stream.CopyToAsync(fileStream);
                
            responseFtp?.Close();
            stream?.Close();
        }
        catch (Exception e)
        {
            Debug.LogError($"Exception: {e.Message}");
            throw;
        }
    }
}

[Serializable]
public struct FilePaths
{
    public string FilePath;
    public string Directory;
    public string FileRemotePath;
}

[Serializable]
public enum AnyBundle
{
    FirstGame,
    SecondGame,
    AnyBundle
}
