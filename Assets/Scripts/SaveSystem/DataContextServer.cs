using System;
using System.Collections.Generic;
using Zenject;

namespace SaveSystem
{
    public class DataContextServer : IPaths
    {
        protected GameDataServer GameDataServerCurrent = new();
        protected FTPServerDownload FtpServerDownload;
        protected Upload Upload;

        public Dictionary<AnyBundle, List<FilePaths>> Paths
        {
            get => GameDataServerCurrent.Paths;
            set
            {
                if (value == null) throw new ArgumentNullException();

                GameDataServerCurrent.Paths = value;
            }
        }

        [Inject]
        private void Construct(FTPServerDownload ftpServerDownload, Upload upload)
        { 
            FtpServerDownload = ftpServerDownload;
            Upload = upload;
        }
    }

    public interface IPaths
    {
        public Dictionary<AnyBundle, List<FilePaths>> Paths { get; }
    }
}