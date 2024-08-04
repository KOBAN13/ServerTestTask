using System;
using UniRx;
using UnityEngine;

public interface IInputSystem
{ 
    Vector2ReactiveProperty Input { get; }
}