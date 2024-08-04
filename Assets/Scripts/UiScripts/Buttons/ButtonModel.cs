using System;
using Cysharp.Threading.Tasks;
using UiScripts.ButtonsType;
using UiScripts.Canvases;

namespace UiScripts
{
    public class ButtonModel : IVisitButtonsType
    {
        private readonly ICanvasEnable _canvasEnable;

        public ButtonModel(ICanvasEnable canvasEnable)
        {
            _canvasEnable = canvasEnable;
        }

        public async void Visit(StartGame buttonStartGame, AnyBundle anyBundle)
        {
            switch (anyBundle)
            {
                case AnyBundle.FirstGame : await buttonStartGame.AddressablesSceneManager.LoadScene("GameFirst");
                     break;
                case AnyBundle.SecondGame : await buttonStartGame.AddressablesSceneManager.LoadScene("GameSecond");
                    break;
            }
        }

        public async void Visit(Load buttonLoad, AnyBundle anyBundle)
        {
            _canvasEnable.IsCanvasesActive.Value = true;
            buttonLoad.Download.DownloadFileFromServer(anyBundle);
            buttonLoad.Download.DownloadFileFromServer(AnyBundle.AnyBundle);
            await UniTask.Delay(TimeSpan.FromSeconds(5f));
            _canvasEnable.IsCanvasesActive.Value = false;
        }

        public void Visit(Unload buttonUnload, AnyBundle anyBundle)
        {
            buttonUnload.Paths.Paths[anyBundle].ForEach(paths => buttonUnload.Delete.DeleteFiles(paths.FilePath, paths.Directory));
            buttonUnload.Paths.Paths[AnyBundle.AnyBundle].ForEach(paths => buttonUnload.Delete.DeleteFiles(paths.FilePath, paths.Directory));
        }
    }

    public interface IVisitButtonsType
    {
        void Visit(StartGame buttonStartGame, AnyBundle anyBundle);
        void Visit(Load buttonLoad, AnyBundle anyBundle);
        void Visit(Unload buttonUnload, AnyBundle anyBundle);
    }
}