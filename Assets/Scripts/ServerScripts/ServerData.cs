using System;
using System.Net;
using UnityEngine;
using Application = UnityEngine.Device.Application;

namespace ServerScripts
{
    public static class ServerData
    {
        private const string FTP_SERVER = "87.242.86.133";
        private const string FTP_USER = "unityuser";
        private const string FTP_USERPASSWORD = "giftrola05";

        private const string LOCAL_PATH_GAME = "C:\\AllGameBundle";
        public static string FilePathGameLocal => LOCAL_PATH_GAME;

        public const string NAME_FIRST_GAME_GROUP_ASSET = "FirstGame";
        public const string NAME_SECOND_GAME_GROUP_ASSET = "SecondGame";
        public const string NAME_ANY_GAME_GROUP_ASSET = "AnyGame";

        public static FtpWebRequest ConnectionToFtpServer(string remoteFilePath, string requestMethod)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri($"ftp://{FTP_SERVER}/{remoteFilePath}"));
            request.Credentials = new NetworkCredential(FTP_USER, FTP_USERPASSWORD);
            request.Method = requestMethod;
            return request;
        }
    }
}