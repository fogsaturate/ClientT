using Godot;
using System;

public partial class Grid : UIComponent
{
    private SettingsProfile settings;

    [Export]
    public MeshInstance3D Cursor { get; set; }

    public override void ApplySettings(SettingsProfile settings)
    {
        this.settings = settings;
    }

    public override void Process(double delta, Attempt state)
    {
        updateCursorPosition(state.CursorPosition.X, state.CursorPosition.Y);
    }

    private void updateCursorPosition(float x, float y)
    {
        Cursor.Position = new Vector3(x, y, 0);
    }
}

