using Godot;
using System;

public partial class Jukebox : Panel
{
    private Label title;

    public override void _Ready()
	{
        title = GetNode<Label>("Title");

        SoundManager.Instance.JukeboxPlayed += UpdateMap;
    }

	public void UpdateMap(Map map)
	{
        title.Text = map.PrettyTitle;
    }
}