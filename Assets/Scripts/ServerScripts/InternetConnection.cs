using System;
using System.Net.NetworkInformation;
using UniRx;
using UnityEngine;
using Zenject;
using Ping = System.Net.NetworkInformation.Ping;

namespace ServerScripts
{
    public class InternetConnection : IInitializable, IDisposable
    {
        private readonly Ping _ping = new();
        private const string ADDRESS = "ya.ru";
        public readonly ReactiveProperty<bool> IsNetworkAvailable = new(true);
        private CompositeDisposable _compositeDisposable = new();

        private async void IsInternetConnectionAvailable()
        {
            try
            {
                PingReply reply = await _ping.SendPingAsync(ADDRESS);
                IsNetworkAvailable.Value = reply.Status == IPStatus.Success;
            }
            catch (Exception e)
            {
                Debug.LogWarning("Internet Connection Lost");
                IsNetworkAvailable.Value = false;
            }
        }

        public void Initialize()
        {
            Observable
                .Timer(TimeSpan.FromSeconds(1f), TimeSpan.FromSeconds(1f))
                .Subscribe(_ => IsInternetConnectionAvailable())
                .AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _ping?.Dispose();
            _compositeDisposable.Clear();
            _compositeDisposable.Dispose();
        }
    }
}