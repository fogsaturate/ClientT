using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Game objects that can be tweened
/// </summary>
public interface ITweenableObject<T>
    where T : TweenObject
{
    Tween CurrentTween { get; }

    List<T> TweenObjects { get; }
}
