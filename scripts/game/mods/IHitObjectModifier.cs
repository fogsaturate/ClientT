using Godot;
using System;

public interface IHitObjectModifier<IHitObject> : IMod
{
    void ModifyHitObject(IHitObject hitObject, Color color, float depth, Attempt attempt);
}
