using Interfases;
using SaveSystem;
using Zenject;

namespace UiScripts.ButtonsType
{
    public class Unload : ButtonsType
    {
        public IDelete Delete { get; private set; }
        public IDownload Download { get; private set; }
        public IPaths Paths { get; private set; }

        [Inject]
        private void Construct(IDelete delete, IPaths paths, IDownload download)
        {
            Delete = delete;
            Paths = paths;
            Download = download;
        }
        
        
        public override void Accept(IVisitButtonsType visitor, AnyBundle anyBundle)
        {
            visitor.Visit(this, anyBundle);
        }
    }
}