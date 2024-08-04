using System;
using System.IO;
using Interfases;

namespace ServerScripts
{
    public class FilesOperation : IDelete
    {
        public void DeleteFiles(string filesPath, string directory)
        {
            if (!Directory.Exists(directory)) throw new Exception("Directory files not exist");
            File.Delete(filesPath);
        }
    }
}