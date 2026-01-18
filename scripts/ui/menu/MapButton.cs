using Godot;
using System;

public partial class MapButton : Panel, ISkinnable
{
	/// <summary>
	/// Parsed map reference
	/// </summary>
    public Map Map;

	/// <summary>
	/// Index within the full map list
	/// </summary>
    public int ListIndex = 0;

	/// <summary>
	/// Minimum Y size (configure in MapList properties)
	/// </summary>
    public float SizeHeight = 90;

	/// <summary>
	/// Additional Y size when hovered (configure in MapList properties)
	/// </summary>
    public float HoverSizeOffset = 10;

	/// <summary>
	/// Additional Y size when selected (configure in MapList properties)
	/// </summary>
    public float SelectedSizeOffset = 20;

	/// <summary>
	/// Total Y size added on top of minimum size, equivalent to HoverSizeOffset + SelectedSizeOffset
	/// </summary>
    public float SizeOffset = 0;

	/// <summary>
	/// Normalized distance from MapList center
	/// </summary>
    public float CenterOffset = 0;

	/// <summary>
	/// Horizontal anchor offset when selected
	/// </summary>
    public float StickoutOffset = 0;

    public bool Hovered = false;
    public bool Selected = false;

    private float targetOutlineFill = 0;
    private float outlineFill = 0;

	[Signal]
    public delegate void PressedEventHandler();

    public ShaderMaterial OutlineShader;

    private Label title;
    private RichTextLabel extra;
    private TextureRect cover;
    private TextureRect favorited;
    private Button button;
    private ShaderMaterial coverMaterial;

    public override void _Ready()
    {
        button = GetNode<Button>("Button");
        title = GetNode<Label>("Title");
        extra = GetNode<RichTextLabel>("Extra");
        cover = GetNode<TextureRect>("Cover");
        favorited = GetNode<TextureRect>("Favorited");
        favorited.Texture = (Texture2D)favorited.Texture.Duplicate();
        coverMaterial = cover.Material as ShaderMaterial;

        Panel outline = GetNode<Panel>("Outline");

        OutlineShader = (ShaderMaterial)outline.Material.Duplicate();
        outline.Material = OutlineShader;

        button.MouseEntered += () => { Hover(true); };
		button.MouseExited += () => { Hover(false); };
		button.Pressed += () => {
			Select();
            EmitSignal(SignalName.Pressed);

            if (SoundManager.Map == null || SoundManager.Map.ID != Map.ID)
            {
                SoundManager.JukeboxQueueInverse.TryGetValue(Map.FilePath.GetFile().GetBaseName(), out int index);
                SoundManager.PlayJukebox(index);
            }
        };

        SkinManager.Instance.Loaded += UpdateSkin;

        UpdateSkin();
    }

    public override void _Process(double delta)
    {
        float smoothCenterOffset = (float)Math.Cos(Math.PI * CenterOffset / 2);

        StickoutOffset = (float)Mathf.Lerp(StickoutOffset, Selected ? 0.05 : 0, Math.Min(1, 16 * delta));
        AnchorLeft = (float)(0.1 - smoothCenterOffset / 20 - StickoutOffset);
        Size = new(Size.X, (float)Mathf.Lerp(Size.Y, SizeHeight + SizeOffset, Math.Min(1, 16 * delta)));
        outlineFill = (float)Mathf.Lerp(outlineFill, targetOutlineFill, Math.Min(1, 10 * delta));

        OutlineShader.SetShaderParameter("fill", outlineFill);

        favorited.RotationDegrees = ListIndex * -10 + (float)Time.GetTicksMsec() / 20;
    }

	public void Hover(bool hover)
	{
        Hovered = hover;
        SizeOffset = computeSizeOffset();
		
        CreateTween().SetTrans(Tween.TransitionType.Quad).TweenProperty(this, "self_modulate", Hovered ? Color.Color8(26, 6, 13, 224) : Color.Color8(0, 0, 0, 224), 0.15);
    }

	public void Select(bool select = true)
	{
        if (select)
        {
            Lobby.Map = Map;

            if (Selected)
            {
                LegacyRunner.Play(Lobby.Map, Lobby.Speed, Lobby.StartFrom, Lobby.Mods);
            }
        }

        Selected = select;
		SizeOffset = computeSizeOffset();

        CreateTween().SetTrans(Tween.TransitionType.Quad).TweenProperty(cover, "modulate", Color.Color8(255, 255, 255, (byte)(Selected ? 255 : 128)), 0.1);
    }

	public void Deselect()
	{
        Select(false);
    }

	public void UpdateInfo(Map map)
	{
        Map = map;
        Name = map.ID;
        
        title.Text = map.PrettyTitle;
        extra.Text = string.Format("[outline_size=2][outline_color=000000][color=808080]{0} — [color={1}]{2} [color=808080]by [color=b0b0b0]{3}",
            Util.String.FormatTime(map.Length / 1000),
            Constants.DIFFICULTY_COLORS[map.Difficulty].ToHtml(),
            map.DifficultyName,
            map.PrettyMappers
        );
        favorited.Visible = MapManager.IsFavorited(map);
        favorited.SelfModulate = Constants.DIFFICULTY_COLORS[map.Difficulty];
    }

	public void UpdateOutline(float targetFill, float fill = -1)
	{
        targetOutlineFill = targetFill;

		if (fill != -1)
		{
            outlineFill = fill;
        }
    }

    public void UpdateSkin(SkinProfile skin = null)
    {
        skin ??= SkinManager.Instance.Skin;

        coverMaterial.Shader = skin.MapButtonCoverShader;
    }

	private float computeSizeOffset()
	{
		return (Hovered ? HoverSizeOffset : 0) + (Selected ? SelectedSizeOffset : 0);
	}
}