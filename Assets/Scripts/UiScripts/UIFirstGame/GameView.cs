using SaveSystem;
using TMPro;
using UiScripts;
using UiScripts.UIFirstGame;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameView : MonoBehaviour
{
    [field: SerializeField] public TextMeshProUGUI Score { get; private set; }
    [field: SerializeField] public Button ButtonClick { get; private set; }
    [field: SerializeField] public ButtonSceneView GoToScene { get; private set; }

    private GameModel _gameModel;
    
    [Inject]
    private void Construct(JsonDataContextLocal jsonData, ButtonSceneModel buttonSceneModel)
    {
        _gameModel = new GameModel(jsonData, this, buttonSceneModel);
    }

    private void Awake()
    {
        SubscribeButton();
    }

    private void OnDestroy()
    {
        _gameModel.Dispose();
    }

    private void SubscribeButton()
    {
        ButtonClick.OnClickAsObservable().Subscribe(_ => _gameModel.AddScore()).AddTo(this);
    }
}
