using DeckBuilderTutorialC.Extensions;
using global::DeckBuilderTutorialC.global;
using Godot;
using Godot.Collections;

namespace DeckBuilderTutorialC;

public partial class CardUI : Control
{
    [Signal]
    public delegate void ReParentRequestedEventHandler(CardUI whichCardUi);

    public static readonly StyleBoxFlat CardBaseStyleBox =
        ResourceLoader.Load<StyleBoxFlat>("res://scenes/ui/card_ui/card_base_stylebox.tres");
    public static readonly StyleBoxFlat CardHoverStyleBox =
        ResourceLoader.Load<StyleBoxFlat>("res://scenes/ui/card_ui/card_hover_stylebox.tres");
    public static readonly StyleBoxFlat CardDraggingStyleBox =
        ResourceLoader.Load<StyleBoxFlat>("res://scenes/ui/card_ui/card_dragging_stylebox.tres");

    [Export]
    public Card Card
    {
        get { return _card; }
        set
        {
            _card = value;
            if (IsNodeReady()) UpdateCardUI();
        }
    }

    Card _card;

    [Export]
    public CharacterStats CharacterStats
    {
        get { return _characterStats; }
        set
        {
            _characterStats = value;
            _characterStats.StatsChanged += OnCharacterStatsChanged;
        }
    }

    CharacterStats _characterStats;

    // @onready variables
    public Panel Panel;
    public Label Cost;
    public TextureRect Icon;
    public Area2D DropPointDetector;
    CardStateMachine _cardStateMachine;
    public Array<Node> Targets;
    public int OriginalIndex;

    public Control Parent;
    public Tween Tween;

    public bool IsPlayable
    {
        get { return _isPlayable; }
        private set
        {
            _isPlayable = value;
            UpdatePlayableUI();
        }
    }

    bool _isPlayable = true;

    public bool IsDisabled;

    public override void _Ready()
    {
        Panel = GetNode<Panel>("Panel");
        Cost = GetNode<Label>("Cost");
        Icon = GetNode<TextureRect>("Icon");
        DropPointDetector = GetNode<Area2D>("DropPointDetector");
        _cardStateMachine = GetNode<CardStateMachine>("CardStateMachine");
        Targets = new Array<Node>();
        OriginalIndex = GetIndex();

        GuiInput += OnGUIInput;
        MouseEntered += OnMouseEntered;
        MouseExited += OnMouseExited;

        DropPointDetector.AreaEntered += OnDropPointDetectorAreaEntered;
        DropPointDetector.AreaExited += OnDropPointDetectorAreaExited;

        Events.Instance.CardAimStarted += OnCardDragOrAimStarted;
        Events.Instance.CardAimEnded += OnCardDragOrAimEnded;
        Events.Instance.CardDragStarted += OnCardDragOrAimStarted;
        Events.Instance.CardDragEnded += OnCardDragOrAimEnded;

        _cardStateMachine.Initialize(this);
    }

    public override void _Input(InputEvent @event)
    {
        _cardStateMachine.OnInput(@event);
    }

    public void Play()
    {
        Card.Play(Targets, CharacterStats);
        QueueFree();
    }

    void OnCharacterStatsChanged()
    {
        IsPlayable = CharacterStats.CanPlayCard(Card);
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

    void OnCardDragOrAimStarted(CardUI cardUsed)
    {
        if (cardUsed == this) return;

        IsDisabled = true;
    }

    void OnCardDragOrAimEnded(CardUI cardUI)
    {
        IsDisabled = false;
        IsPlayable = CharacterStats.CanPlayCard(Card);
    }

    public void AnimateToPosition(Vector2 newPosition, float duration)
    {
        Tween = CreateTween().SetTrans(Tween.TransitionType.Circ).SetEase(Tween.EaseType.Out);
        Tween.TweenProperty(this, "global_position", newPosition, duration);
    }

    void UpdateCardUI()
    {
        if (Card == null ) return;
        
        Cost.Text = $"{Card.Cost}";
        Icon.Texture = Card.Icon;
    }

    void UpdatePlayableUI()
    {
        if (!IsInstanceValid(Cost) || !IsInstanceValid(Icon)) return;

        if (IsPlayable)
        {
            Cost.RemoveThemeColorOverride("font_color");
            Icon.Modulate = new Color(1, 1, 1);
        }
        else
        {
            Cost.AddThemeColorOverride("font_color", new Color("#eb4034"));
            Icon.Modulate = new Color(1, 1, 1, 0.5f);
        }
    }
}
