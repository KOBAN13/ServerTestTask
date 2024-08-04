using System;
using UniRx;
using UnityEngine;
using Zenject;

public class InputSystemPC : IInputSystem, IInitializable, IDisposable
{
    private InputSystem _input;
    private CompositeDisposable _compositeDisposable = new();
    public Vector2ReactiveProperty Input { get; private set; } = new();

    public InputSystemPC(InputSystem input)
    {
        _input = input;
    }

    private void GetMovement()
    {
        Input.Value = _input.Movement.Move.ReadValue<Vector2>();
    }

    public void Initialize()
    {
        _input.Enable();
        
        Observable
            .EveryUpdate()
            .Subscribe(_ =>
            {
                GetMovement();
            })
            .AddTo(_compositeDisposable);
        
    }

    public void Dispose()
    {
        _input.Disable();
        _input.Dispose();
        _compositeDisposable.Clear();
        _compositeDisposable.Dispose();
    }
}
