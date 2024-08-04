using UnityEngine;

namespace UiScripts.Canvases
{
    public class CanvasView : MonoBehaviour
    {
        [field: SerializeField] public Canvas Menu { get; private set; }
        [field: SerializeField] public Canvas LoadingResources { get; private set; }
        [field: SerializeField] public Canvas InternetConnection { get; private set; }
    }
}