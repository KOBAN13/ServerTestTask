using SaveSystem;
using ServerScripts;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace DI
{
    public class SaveSystemsInject : MonoInstaller
    {
        [SerializeField] private FTPServerDownload ftpServerDownload;
        [SerializeField] private Upload _upload;
        public override void InstallBindings()
        {
            BindServerOperation();
        }

        private void BindServerOperation()
        {
            Container.BindInterfacesAndSelfTo<FTPServerDownload>().FromInstance(ftpServerDownload).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<JsonDataContextServer>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Upload>().FromInstance(_upload).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<FilesOperation>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<InternetConnection>().AsSingle().NonLazy();
        }
    }
}