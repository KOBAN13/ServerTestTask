using System;
using UniRx;
using Zenject;

namespace Character
{
    public class Player : IInitializable, IDisposable
    {
        private readonly IMovable _movable;
        private readonly IInputSystem _input;
        private readonly CompositeDisposable _compositeDisposable = new();

        public Player(IMovable movable, IInputSystem input)
        {
            _movable = movable;
            _input = input;
        }
        
        public void Dispose()
        {
            _compositeDisposable.Clear();
            _compositeDisposable.Dispose();
        }

        public void Initialize()
        {
            _input.Input.Subscribe(_ => _movable.Move(_input.Input.Value, 15f)).AddTo(_compositeDisposable);
        }
    }
}