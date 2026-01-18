using Godot;
using System.IO;

public partial class BaseSpace : Node3D
{
    public bool Playing = false;

    public Camera3D Camera;
    public WorldEnvironment WorldEnvironment;
    public ImageTexture Cover;

    public override void _Ready()
    {
        base._Ready();

        Camera = GetNode<Camera3D>("Camera3D");
        WorldEnvironment = GetNode<WorldEnvironment>("WorldEnvironment");
    }

    public virtual void UpdateMap(Map map)
    {
        Cover = null;

        // until covers are lazyloaded in the map db
        if (map.CoverBuffer != null)
        {
            string tempPath = $"{Constants.USER_FOLDER}/cache/info_cover{map.ID}.png";
            Godot.FileAccess file = Godot.FileAccess.Open(tempPath, Godot.FileAccess.ModeFlags.WriteRead);
            file.StoreBuffer(map.CoverBuffer);
			file.Close();
            ImageTexture tex = ImageTexture.CreateFromImage(Image.LoadFromFile(tempPath));
            File.Delete(tempPath);

            Cover = tex;
        }
        //
    }

    public virtual void UpdateState(bool playing)
    {
        Playing = playing;
    }
}