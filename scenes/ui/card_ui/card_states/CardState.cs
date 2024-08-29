using Godot;

namespace DeckBuilderTutorialC;

public abstract partial class CardState : Node
{
    public enum EState
    {
        Base,
        Clicked,
        Dragging,
        Aiming,
        Released
    }

    [Signal]
    public delegate void TransitionRequestedEventHandler(EState from, EState to);

    [Export]
    public EState State;

    public CardUI CardUI;

    public abstract void Enter();

    public abstract void Exit();

    public abstract void OnInput(InputEvent @event);

    public abstract void OnGUIInput(InputEvent @event);

    public abstract void OnMouseEntered();

    public abstract void OnMouseExited();
}
