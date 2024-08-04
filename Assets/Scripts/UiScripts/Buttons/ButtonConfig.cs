using System;
using UnityEngine;
using UnityEngine.UI;

namespace UiScripts
{
    [Serializable]
    public struct ButtonConfig
    {
        [field: SerializeField] public ButtonsType.ButtonsType ButtonType { get; private set; }
        [field: SerializeField] public AnyBundle AnyBundle { get; private set; }
        [field: SerializeField] public Button Button { get; private set; }
    }
    
}