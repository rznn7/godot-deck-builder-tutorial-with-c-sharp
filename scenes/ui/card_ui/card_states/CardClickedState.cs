using Godot;

namespace DeckBuilderTutorialC;

public partial class CardClickedState : CardState
{
    public override void Enter()
    {
        CardUI.DropPointDetector.Monitoring = true;
    }

    public override void Exit()
    {
    }

    public override void OnInput(InputEvent @event)
    {
        if (@event is InputEventMouseMotion)
        {
            EmitSignal(CardState.SignalName.TransitionRequested, (int)EState.Clicked, (int)EState.Dragging);
        }
    }

    public override void OnGUIInput(InputEvent @event)
    {
    }

    public override void OnMouseEntered()
    {
    }

    public override void OnMouseExited()
    {
    }
}
