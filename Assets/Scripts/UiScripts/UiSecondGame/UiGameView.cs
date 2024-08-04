using System;
using Interfases;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UiScripts.UiSecondGame
{
    public class UiGameView : MonoBehaviour, IInitAfterLoadResources
    {
        private UiGameModel _uiGameModel;
        [field: SerializeField] public TextMeshProUGUI TextCurrentTime { get; private set; }
        [field: SerializeField] public TextMeshProUGUI TextBestTime { get; private set; }
        [field: SerializeField] public Button Menu { get; private set; }
        [field: SerializeField] public Canvas Buttons { get; private set; }


        private void SubscribeButton()
        {
            Menu.OnClickAsObservable().Subscribe(_ => _uiGameModel.LoadScene("Menu")).AddTo(this);
        }
        
        public void Init<T>(T mono)
        {
            _uiGameModel = mono as UiGameModel;
            SubscribeButton();
        }
    }
}