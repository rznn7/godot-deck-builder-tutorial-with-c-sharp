using System.Linq;
using DeckBuilderTutorialC.Extensions;
using global::DeckBuilderTutorialC.global;
using Godot;

namespace DeckBuilderTutorialC;

public partial class PlayerHandler : Node
{
    const float HandDrawInterval = 0.25f;
    const float HandDiscardInterval = 0.25f;

    [Export]
    public Hand Hand { get; set; }

    CharacterStats _charStats;

    public override void _Ready()
    {
        Events.Instance.CardPlayed += OnCardPlayed;
    }
    
    public void StartBattle(CharacterStats charStats)
    {
        _charStats = charStats;

        charStats.DrawPile = charStats.Deck.Duplicate<CardPile>(true);
        charStats.DrawPile.Shuffle();
        charStats.Discard = new CardPile();

        StartTurn();
    }

    public void StartTurn()
    {
        _charStats.Block = 0;
        _charStats.ResetMana();
        DrawCards(_charStats.CardsPerTurns);
    }

    public void EndTurn()
    {
        Hand.DisableHand();
        DiscardCards();
    }
    
    void OnCardPlayed(Card card)
    {
        _charStats.Discard.AddCard(card);
    }

    void DiscardCards()
    {
        var tween = CreateTween();

        foreach (var cardUI in Hand.GetChildren().OfType<CardUI>())
        {
            tween.TweenCallback(Callable.From(() => _charStats.Discard.AddCard(cardUI.Card)));
            tween.TweenCallback(Callable.From(() => Hand.DiscardCard(cardUI)));
            tween.TweenInterval(HandDiscardInterval);
        }
        
        tween.Finished += () => Events.Instance.EmitSignal(Events.SignalName.PlayerHandDiscarded);
    }

    void DrawCard()
    {
        ReshuffleDeckFromDiscard();
        Hand.AddCard(_charStats.DrawPile.DrawCard());
        ReshuffleDeckFromDiscard();
    }

    void ReshuffleDeckFromDiscard()
    {
        if (!_charStats.DrawPile.IsEmpty) return;

        while (!_charStats.Discard.IsEmpty)
        {
            _charStats.DrawPile.AddCard(_charStats.Discard.DrawCard());
        }
        
        _charStats.DrawPile.Shuffle();
    }

    void DrawCards(int amount)
    {
        var tween = CreateTween();

        for (var i = 0; i < amount; i++)
        {
            tween.TweenCallback(Callable.From(DrawCard));
            tween.TweenInterval(HandDrawInterval);
        }

        tween.Finished += () => { Events.Instance.EmitSignal(Events.SignalName.PlayerHandDrawn); };
    }
}
