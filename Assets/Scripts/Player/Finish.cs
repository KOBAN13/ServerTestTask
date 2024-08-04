using Interfases;
using UiScripts.UiSecondGame;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Character
{
    public class Finish : MonoBehaviour, IInitAfterLoadResources
    {
        private IPlayerFinish _bestTime;
        
        private void Start()
        {
            PlayerComponents playerComponents;
            this
                .OnTriggerEnterAsObservable()
                .Where(collider => collider.TryGetComponent(out playerComponents))
                .Subscribe(_ => _bestTime.OnPlayerFinish())
                .AddTo(this);
        }

        public void Init<T>(T mono)
        {
            _bestTime = mono as IPlayerFinish;
        }
    }
}