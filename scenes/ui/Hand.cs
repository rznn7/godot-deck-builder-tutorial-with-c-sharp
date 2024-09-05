using System.Linq;
using global::DeckBuilderTutorialC.global;
using Godot;

namespace DeckBuilderTutorialC;

public partial class Hand : HBoxContainer
{
    [Export]
    public CharacterStats CharacterStats { get; set; }

    // @onready variables
    static readonly PackedScene CardUiScene = GD.Load<PackedScene>("res://scenes/ui/card_ui.tscn");

    int _cardsPlayedThisTurn;

    public override void _Ready()
    {
        Events.Instance.CardPlayed += OnCardPlayed;
    }

    public void AddCard(Card card)
    {
        var newCardUi = CardUiScene.Instantiate<CardUI>();
        AddChild(newCardUi);
        newCardUi.ReParentRequested += OnCardUIReParentRequested;
        newCardUi.Card = card;
        newCardUi.Parent = this;
        newCardUi.CharacterStats = CharacterStats;
    }

    public void DiscardCard(CardUI cardUI)
    {
        cardUI.QueueFree();
    }

    public void DisableHand()
    {
        foreach (var cardUI in GetChildren().OfType<CardUI>())
        {
            cardUI.IsDisabled = true;
        }
    }

    void OnCardPlayed(Card card)
    {
        _cardsPlayedThisTurn++;
    }

    void OnCardUIReParentRequested(CardUI child)
    {
        child.Reparent(this);
        var newIndex = Mathf.Clamp(child.OriginalIndex - _cardsPlayedThisTurn, 0, GetChildCount());
        CallDeferred("move_child", child, newIndex);
    }
}
