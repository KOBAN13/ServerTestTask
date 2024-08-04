using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Loader
{
    public class ReferenceLoadAssetSecondGame : MonoBehaviour
    {
        [field: SerializeField] public AssetLabelReference Prefabs { get; private set; }
    }
}