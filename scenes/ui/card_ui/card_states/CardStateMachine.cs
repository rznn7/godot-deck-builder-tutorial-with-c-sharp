using System.Linq;
using Godot;
using Godot.Collections;

namespace DeckBuilderTutorialC;

public partial class CardStateMachine : Node
{
    [Export]
    public CardState InitialState;

    CardState _currentState;
    Dictionary<CardState.EState, CardState> _states = new();

    public void Initialize(CardUI card)
    {
        foreach (var cardStateNode in GetChildren().OfType<CardState>())
        {
            _states[cardStateNode.State] = cardStateNode;
            cardStateNode.TransitionRequested += OnTransitionRequested;
            cardStateNode.CardUI = card;
        }

        InitialState?.Enter();
        _currentState = InitialState;
    }

    public void OnInput(InputEvent @event)
    {
        _currentState?.OnInput(@event);
    }

    public void OnGUIInput(InputEvent @event)
    {

        _currentState?.OnGUIInput(@event);
    }

    public void OnMouseEntered()
    {
        _currentState?.OnMouseEntered();
    }

    public void OnMouseExited()
    {
        _currentState?.OnMouseExited();
    }

    void OnTransitionRequested(CardState.EState from, CardState.EState to)
    {
        if (from != _currentState.State) return;

        if (_states.TryGetValue(to, out var newState))
        {
            _currentState?.Exit();
            newState.Enter();
            _currentState = newState;
        }
    }
}
