using System;
using UnityEngine;

namespace UiScripts.ButtonsType
{
    [Serializable]
    public abstract class ButtonsType : MonoBehaviour
    {
        public abstract void Accept(IVisitButtonsType visitor, AnyBundle anyBundle);
    }
}