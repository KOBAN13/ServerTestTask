using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UiScripts
{
    public class ButtonSceneView : MonoBehaviour
    {
        private ButtonSceneModel _buttonSceneModel;
        [field: SerializeField] public Button GoToMenu { get; private set; }

        [Inject]
        private void Construct(ButtonSceneModel buttonSceneModel)
        {
            _buttonSceneModel = buttonSceneModel;
        }

        private void Awake()
        {
            GoToMenu.OnClickAsObservable().Subscribe(_ => _buttonSceneModel.LoadMenuScene()).AddTo(this);
        }
    }
}