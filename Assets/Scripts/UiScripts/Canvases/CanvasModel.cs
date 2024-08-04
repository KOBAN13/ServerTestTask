using System;
using ServerScripts;
using UniRx;
using UnityEngine;
using Zenject;

namespace UiScripts.Canvases
{
    public class CanvasModel : IInitializable, IDisposable, ICanvasEnable
    {
        private readonly CanvasView _canvasView;
        private readonly InternetConnection _internetConnection;
        private readonly CompositeDisposable _compositeDisposable = new();

        public ReactiveProperty<bool> IsCanvasesActive { get; } = new();

        public CanvasModel(CanvasView canvasView, InternetConnection internetConnection)
        {
            _canvasView = canvasView;
            _internetConnection = internetConnection;
        }

        public void Dispose()
        {
            _compositeDisposable.Clear();
            _compositeDisposable.Dispose();
        }

        public void Initialize()
        {
            IsCanvasesActive.Subscribe(_ =>
            {
                _canvasView.Menu.enabled = !IsCanvasesActive.Value;
                _canvasView.LoadingResources.enabled = IsCanvasesActive.Value;
            }).AddTo(_compositeDisposable);

            _internetConnection.IsNetworkAvailable.Subscribe(_ =>
            {
                _canvasView.InternetConnection.enabled = !_internetConnection.IsNetworkAvailable.Value;
                _canvasView.Menu.enabled = _internetConnection.IsNetworkAvailable.Value;
                Debug.LogWarning(_internetConnection.IsNetworkAvailable.Value);
            }).AddTo(_compositeDisposable);
        }
    }

    public interface ICanvasEnable
    {
        ReactiveProperty<bool> IsCanvasesActive { get; }
    }
}