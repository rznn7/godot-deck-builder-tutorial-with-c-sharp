using Godot;

namespace DeckBuilderTutorialC.global;

public partial class Events : Node
{
    public static Events Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
    }

    // Card related events

    [Signal]
    public delegate void CardDragStartedEventHandler(CardUI cardUI);

    [Signal]
    public delegate void CardDragEndedEventHandler(CardUI cardUI);

    [Signal]
    public delegate void CardAimStartedEventHandler(CardUI cardUI);

    [Signal]
    public delegate void CardAimEndedEventHandler(CardUI cardUI);

    [Signal]
    public delegate void CardPlayedEventHandler(Card card);

    [Signal]
    public delegate void CardTooltipShowRequestedEventHandler(Card card);

    [Signal]
    public delegate void CardTooltipHideRequestedEventHandler();

    // Player-related events

    [Signal]
    public delegate void PlayerHandDrawnEventHandler();
    
    [Signal]
    public delegate void PlayerHandDiscardedEventHandler();
    
    [Signal]
    public delegate void PlayerTurnEndedEventHandler();
}
