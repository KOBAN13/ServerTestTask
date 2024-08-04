using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Cysharp.Threading.Tasks;
using SaveSystem;
using ServerScripts;
using UnityEngine;
using Zenject;

public class Upload : MonoBehaviour
{
    private Dictionary<AnyBundle, List<FilePaths>> _paths;
    
    private JsonDataContextServer _jsonDataContextServer;

    [Inject]
    private void Construct(JsonDataContextServer jsonDataContextServer) =>
        _jsonDataContextServer = jsonDataContextServer;
    
    public async UniTask UploadToServer()
    {
        _paths = new Dictionary<AnyBundle, List<FilePaths>>
        {
            { AnyBundle.FirstGame , new List<FilePaths>()},
            { AnyBundle.SecondGame , new List<FilePaths>()},
            { AnyBundle.AnyBundle , new List<FilePaths>()}
        };
        
        UploadFileToServer();
        _jsonDataContextServer.Paths = _paths;
        await _jsonDataContextServer.Save();
        Debug.LogWarning("Operation Upload File To Server Success");
    }

    private void UploadFileToServer()
    {
        var gameBundle = FindBundleFile(ServerData.FilePathGameLocal);

        gameBundle.ForEach(async x => await UploadFile(x.FileRemotePath, x.FilePath));
    }

    private async UniTask UploadFile(string remoteFilePath, string filePath)
    {
        if (!File.Exists($"{filePath}"))
        {
            Debug.LogError("Файл не найден");
            return;
        }

        try
        {
            FtpWebRequest request = ServerData.ConnectionToFtpServer(remoteFilePath, WebRequestMethods.Ftp.UploadFile);

            await using (var stream = request.GetRequestStream())
            {
                await using (var fileStream = File.OpenRead($"{filePath}"))
                {
                    await fileStream.CopyToAsync(stream);
                }
            }

            FtpWebResponse response = (FtpWebResponse)await request.GetResponseAsync().AsUniTask();
        }
        catch (Exception e)
        {
            Debug.LogError($"Ошибка {e.Message}");
            throw;
        }
    }
    
    public async UniTask UploadJson(string localFilePath, string remoteFilePath)
    {
        try
        {
            FtpWebRequest request = ServerData.ConnectionToFtpServer(remoteFilePath, WebRequestMethods.Ftp.UploadFile);

            var bytes = await File.ReadAllBytesAsync(localFilePath);
            
            await using (var stream = request.GetRequestStream())
            {
                await stream.WriteAsync(bytes);
            }

            FtpWebResponse response = (FtpWebResponse)await request.GetResponseAsync().AsUniTask();
            
            File.Delete(localFilePath);
            response.Close();
        }
        catch (Exception e)
        {
            Debug.LogError($"Ошибка {e.Message}");
            throw;
        }
    }

    private List<FilePaths> FindBundleFile(string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
        {
            Debug.LogError(directoryPath + " Dont have");
        }
        var allFiles = Directory.GetFiles(directoryPath);
        List<FilePaths> listBundleFile = new List<FilePaths>(); 

        foreach (var file in allFiles)
        {
            if (Path.GetExtension(file).ToLower() != ".bundle") continue;
            var pathSplit = file.Split('\\');
            
            CheckTypeGameFile(pathSplit, file, listBundleFile);
        }

        return listBundleFile;
    }

    private void CheckTypeGameFile(string[] pathSplit, string file, List<FilePaths> listBundleFile)
    {
        FilePaths filePath = default;
        
        if (pathSplit[^1]
            .StartsWith(ServerData.NAME_FIRST_GAME_GROUP_ASSET.ToLower().Substring(0, ServerData.NAME_FIRST_GAME_GROUP_ASSET.Length)))
        {
            FindBundleFileTypeGame(filePath, file, listBundleFile, ServerData.NAME_FIRST_GAME_GROUP_ASSET, AnyBundle.FirstGame);
        }
        else if (pathSplit[^1]
                 .StartsWith(ServerData.NAME_SECOND_GAME_GROUP_ASSET.ToLower().Substring(0, ServerData.NAME_SECOND_GAME_GROUP_ASSET.Length)))
        {
            FindBundleFileTypeGame(filePath, file, listBundleFile, ServerData.NAME_SECOND_GAME_GROUP_ASSET, AnyBundle.SecondGame);
        }
        else
        {
            FindBundleFileTypeGame(filePath, file, listBundleFile, ServerData.NAME_ANY_GAME_GROUP_ASSET, AnyBundle.AnyBundle);
        }
    }

    private void FindBundleFileTypeGame(FilePaths filePath, string file, List<FilePaths> listBundleFile, string groupAsset, AnyBundle anyBundle)
    {
        filePath = new FilePaths
        {
            Directory = Path.GetDirectoryName(file), FilePath = Path.GetFullPath(file),
            FileRemotePath = $"{groupAsset}/{Path.GetFileName(file)}"
        };
        listBundleFile.Add(filePath);
        _paths[anyBundle].Add(filePath);
    }
}

