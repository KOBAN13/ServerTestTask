using AddressablesManagement;
using Zenject;

namespace UiScripts.ButtonsType
{
    public class StartGame : ButtonsType
    {
        public AddressablesSceneManager AddressablesSceneManager { get; private set; }

        [Inject]
        private void Construct(AddressablesSceneManager addressablesManagement) =>
            AddressablesSceneManager = addressablesManagement;
        
        public override void Accept(IVisitButtonsType visitor, AnyBundle anyBundle)
        {
            visitor.Visit(this, anyBundle);
        }
    }
}