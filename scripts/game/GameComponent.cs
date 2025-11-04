using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class GameComponent : Node3D
{
    [Export]
    public Array<UIComponent> InterfaceComponents { get; set; }

    [Export]
    public Array<Renderer<ITimelineObject>> Renderers { get; set; }

    public Attempt CurrentAttempt { get; private set; } = new();

    public override void _Ready()
    {
        ApplySettings(CurrentAttempt.Settings);
    }

    public override void _Process(double delta)
    {
        // Modify Attempt based on game logic loop
        // ProcessLogic(CurrentAttempt);

        // Update rendering (notes/objects) on attempt state
        foreach (var renderer in Renderers)
        {
            renderer.Process(delta, CurrentAttempt);
        }

        // Update interface components based on attempt state
        foreach (var component in InterfaceComponents)
        {
            component.Process(delta, CurrentAttempt);
        }

    }

    public void ApplySettings(SettingsProfile settings)
    {
        CurrentAttempt.Settings = settings;

        foreach (var component in InterfaceComponents)
        {
            component.ApplySettings(settings);
        }

        foreach (var renderer in Renderers)
        {
            renderer.ApplySettings(settings);
        }
    }
}
