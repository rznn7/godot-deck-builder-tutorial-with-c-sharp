using global::DeckBuilderTutorialC.global;
using Godot;

namespace DeckBuilderTutorialC;

public partial class Tooltip : PanelContainer
{
    [Export]
    public float FadeSeconds { get; set; } = 0.2f;

    // @onready variables
    public TextureRect TooltipIcon;
    public RichTextLabel TooltipTextLabel;

    Tween _tween;
    bool _isVisible;

    public override void _Ready()
    {
        TooltipIcon = GetNode<TextureRect>("MarginContainer/VBoxContainer/TooltipIcon");
        TooltipTextLabel = GetNode<RichTextLabel>("MarginContainer/VBoxContainer/TooltipText");

        Events.Instance.CardTooltipShowRequested += ShowTooltip;
        Events.Instance.CardTooltipHideRequested += HideTooltip;

        Modulate = new Color(0, 0, 0, 0);
    }

    void ShowTooltip(Card card)
    {
        _isVisible = true;
        _tween?.Kill();

        TooltipIcon.Texture = card.Icon;
        TooltipTextLabel.Text = card.TooltipText;

        ShowAnimation();
    }

    void ShowAnimation()
    {

        _tween = CreateTween().SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Cubic);
        _tween.TweenCallback(Callable.From(Show));
        _tween.TweenProperty(this, "modulate", new Color(1, 1, 1), FadeSeconds);
    }

    void HideTooltip()
    {
        _isVisible = false;
        _tween?.Kill();

        GetTree().CreateTimer(FadeSeconds, false).Timeout += HideAnimation;
    }

    void HideAnimation()
    {
        if (_isVisible) return;

        _tween = CreateTween().SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Cubic);
        _tween.TweenProperty(this, "modulate", new Color(0, 0, 0, 0), FadeSeconds);
        _tween.TweenCallback(Callable.From(Hide));
    }
}
