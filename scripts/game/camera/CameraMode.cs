using Godot;
using System;

public abstract class CameraMode
{
    public abstract bool Rankable { get; }

    public abstract void Process(Attempt attempt, Camera3D camera, Vector2 mouseDelta);
}
