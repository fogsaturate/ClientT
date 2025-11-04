using Godot;
using System;
using System.Collections.Generic;

public class Attempt
{
    public bool Paused { get; set; }

    public Map Map { get; set; }

    public double Progress { get; set; }

    public Vector2 CursorPosition { get; set; } = new();

    public int Speed { get; set; }

    public HashSet<string> Mods { get; set; }

    public List<Note> Notes { get; set; }

    public List<ITimelineObject> Objects { get; set; }

    public SettingsProfile Settings { get; set; }
}
