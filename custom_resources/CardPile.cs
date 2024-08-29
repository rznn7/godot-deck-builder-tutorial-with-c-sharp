using System.Linq;
using DeckBuilderTutorialC.Extensions;
using Godot;
using Godot.Collections;

namespace DeckBuilderTutorialC;

[GlobalClass]
public partial class CardPile : Resource
{
    [Signal]
    public delegate void CardPileSizeChangedEventHandler(int cardAmount);

    [Export]
    public Array<Card> Cards = new();

    public bool IsEmpty
    {
        get { return Cards.Count == 0; }
    }

    public Card DrawCard()
    {
        var card = Cards.PopFront();
        EmitSignal(SignalName.CardPileSizeChanged, Cards.Count);
        return card;
    }

    public void AddCard(Card card)
    {
        Cards.Add(card);
        EmitSignal(SignalName.CardPileSizeChanged, Cards.Count);
    }

    public void Shuffle()
    {
        Cards.Shuffle();
    }

    public void Clear()
    {
        Cards.Clear();
    }

    public override string ToString()
    {
        return string.Join("\n", Cards.Select((card, index) => $"{index + 1}: {card.Id}"));
    }
}
