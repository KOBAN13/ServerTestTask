using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

namespace UiScripts
{
    public class ButtonsView : MonoBehaviour
    {
        [field: SerializeField] public List<ButtonConfig> ButtonViews { get; private set; }
        
        [Inject]
        public void Construct(ButtonModel model)
        {
            foreach (var buttonConfig in ButtonViews)
            {
                SubscribeButton(buttonConfig, model);
            }
        }

        private void SubscribeButton(ButtonConfig buttonConfig, ButtonModel model)
        {
            buttonConfig.Button.OnClickAsObservable()
                .Subscribe(_ => buttonConfig.ButtonType.Accept(model, buttonConfig.AnyBundle))
                .AddTo(this);
        }
    }
}