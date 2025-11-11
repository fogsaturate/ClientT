using System.Collections;
using System.Collections.Generic;
using Godot;
using Godot.Collections;
using static LegacyRunner;

[GlobalClass]
public partial class GameComponent : Node3D
{
    [Export]
    public bool Standalone { get; set; } = true;

    [Export]
    public Camera3D Camera { get; set; }

    [Export]
    public Array<UIComponent> InterfaceComponents { get; set; }

    [Export]
    public Array<Renderer> Renderers { get; set; }

    public bool Playing { get; private set; } = true;

    public Attempt CurrentAttempt { get; private set; } = new();

    public void Play(Attempt attempt)
    {
        ApplySettings(attempt.Settings);
    }

    public override void _Ready()
    {
        ApplySettings(CurrentAttempt.Settings);

        // Automatically attempt to start the game if standalone
        if (Standalone)
        {
            Input.MouseMode = CurrentAttempt.Settings.AbsoluteInput ? Input.MouseModeEnum.ConfinedHidden : Input.MouseModeEnum.Captured;
            Input.UseAccumulatedInput = false;
        }
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

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion eventMouseMotion && Playing)
        {
            CurrentAttempt.CameraMode.Process(CurrentAttempt, Camera, eventMouseMotion.Relative);

            CurrentAttempt.DistanceMM += eventMouseMotion.Relative.Length() / CurrentAttempt.Settings.Sensitivity / 57.5;
        }
    }
}
