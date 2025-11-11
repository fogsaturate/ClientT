using Godot;
using System;

public class GhostMod : Mod, IHitObjectModifier<Note>
{
    public override string Name => "Ghost";

    public void ModifyHitObject(Note note, Color color, float depth, Attempt attempt)
    {
        float ad = (float)attempt.Settings.ApproachDistance;

        color.A -= Mathf.Min(1, (ad - depth) / (ad / 2));
    }
}
