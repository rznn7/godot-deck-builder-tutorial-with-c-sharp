using Godot;

namespace DeckBuilderTutorialC.global;

public partial class Events : Node
{
    public static Events Instance { get; private set; }
    
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

    public override void _Ready()
    {
        Instance = this;
    }
}
