using DeckBuilderTutorialC.Extensions;
using Godot;

namespace DeckBuilderTutorialC;

[GlobalClass]
public partial class CharacterStats : Stats
{
    [Export]
    public CardPile StartingDeck;

    [Export]
    public int CardsPerTurns;

    [Export]
    public int MaxMana;

    int _mana;

    public int Mana
    {
        get { return _mana; }
        set
        {
            _mana = value;
            EmitSignal(Stats.SignalName.StatsChanged);
        }
    }

    public CardPile Deck;
    public CardPile Discard;
    public CardPile DrawPile;

    public void ResetMana()
    {
        Mana = MaxMana;
    }

    public bool CanPlayCard(Card card)
    {
        return Mana >= card.Cost;
    }

    public override CharacterStats CreateInstance()
    {
        if (base.CreateInstance() is not CharacterStats instance) return null;

        instance.ResetMana();
        instance.Deck = instance.StartingDeck.Duplicate<CardPile>();
        instance.DrawPile = new CardPile();
        instance.Discard = new CardPile();

        return instance;
    }
}
