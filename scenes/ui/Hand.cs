using System.Linq;
using global::DeckBuilderTutorialC.global;
using Godot;

namespace DeckBuilderTutorialC;

public partial class Hand : HBoxContainer
{
    int _cardsPlayedThisTurn;

    public override void _Ready()
    {
        Events.Instance.CardPlayed += OnCardPlayed;

        foreach (var cardUINode in GetChildren().OfType<CardUI>())
        {
            cardUINode.Parent = this;
            cardUINode.ReParentRequested += OnCardUIReParentRequested;
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
