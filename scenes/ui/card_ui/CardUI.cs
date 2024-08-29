using Godot;
using Godot.Collections;

namespace DeckBuilderTutorialC;

public partial class CardUI : Control
{
    [Signal]
    public delegate void ReParentRequestedEventHandler(CardUI whichCardUi);

    [Export]
    public Card Card;

    public ColorRect ColorRect;
    public Label StateLabel;
    public Area2D DropPointDetector;
    public Array<Node> Targets;
    public Control Parent;
    public Tween Tween;

    CardStateMachine _cardStateMachine;

    public override void _Ready()
    {
        ColorRect = GetNode<ColorRect>("Color");
        StateLabel = GetNode<Label>("State");
        DropPointDetector = GetNode<Area2D>("DropPointDetector");
        Targets = new Array<Node>();
        _cardStateMachine = GetNode<CardStateMachine>("CardStateMachine");

        GuiInput += OnGUIInput;
        MouseEntered += OnMouseEntered;
        MouseExited += OnMouseExited;

        DropPointDetector.AreaEntered += OnDropPointDetectorAreaEntered;
        DropPointDetector.AreaExited += OnDropPointDetectorAreaExited;

        _cardStateMachine.Initialize(this);
    }

    public override void _Input(InputEvent @event)
    {
        _cardStateMachine.OnInput(@event);
    }

    void OnGUIInput(InputEvent @event)
    {
        _cardStateMachine.OnGUIInput(@event);
    }

    void OnMouseEntered()
    {
        _cardStateMachine.OnMouseEntered();
    }

    void OnMouseExited()
    {
        _cardStateMachine.OnMouseExited();
    }

    void OnDropPointDetectorAreaEntered(Area2D area)
    {
        if (!Targets.Contains(area))
        {
            Targets.Add(area);
        }
    }

    void OnDropPointDetectorAreaExited(Area2D area)
    {
        Targets.Remove(area);
    }

    public void AnimateToPosition(Vector2 newPosition, float duration)
    {
        Tween = CreateTween().SetTrans(Tween.TransitionType.Circ).SetEase(Tween.EaseType.Out);
        Tween.TweenProperty(this, "global_position", newPosition, duration);
    }
}
