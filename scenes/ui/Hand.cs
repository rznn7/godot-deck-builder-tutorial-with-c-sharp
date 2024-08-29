using System.Linq;
using Godot;

namespace DeckBuilderTutorialC;

public partial class Hand : HBoxContainer
{
    public override void _Ready()
    {
        foreach (var cardUINode in GetChildren().OfType<CardUI>())
        {
            cardUINode.Parent = this;
            cardUINode.ReParentRequested += OnCardUIReParentRequested;
        }
    }

    void OnCardUIReParentRequested(CardUI whichCardUi)
    {
        whichCardUi.Reparent(this);
    }
}
