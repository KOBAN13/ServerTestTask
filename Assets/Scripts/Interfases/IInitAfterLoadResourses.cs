using UnityEngine;

namespace Interfases
{
    public interface IInitAfterLoadResources
    {
        void Init<T>(T mono);
    }
}