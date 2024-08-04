using Interfases;
using Zenject;

namespace UiScripts.ButtonsType
{
    public class Load : ButtonsType
    {
        public IDownload Download { get; private set; }

        [Inject]
        private void Construct(IDownload download)
        {
            Download = download;
        }
        
        public override void Accept(IVisitButtonsType visitor, AnyBundle anyBundle)
        {
            visitor.Visit(this, anyBundle);
        }
    }
}