using System;
using Interfases;
using UniRx;
using UnityEngine;
using Zenject;

namespace Character
{
    public class Movement : IMovable, IInitializable, IDisposable, IInitAfterLoadResources
    {
        private PlayerComponents _playerComponents;
        private Vector3 _targetDirection;
        private Vector2 _input;
        private float _speed;
        private bool _isInit;
        private CompositeDisposable _compositeDisposable = new();
        
        public void Move(Vector2 input, float speed)
        {
            _input = input;
            _speed = speed;
        }

        public void Initialize()
        {
            Observable.EveryUpdate().Where(_ => _isInit == true).Subscribe(_ =>
            {
                _targetDirection.x = _speed * _input.x;
                _targetDirection.z = _speed * _input.y;
                _playerComponents.CharacterController?.Move(_targetDirection * Time.deltaTime);
            }).AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable.Clear();
            _compositeDisposable.Dispose();
        }

        public void Init<T>(T mono)
        {
            _playerComponents = mono as PlayerComponents;
            _isInit = true;
        }
    }

    public interface IMovable
    {
        void Move(Vector2 input, float speed);
    }
}