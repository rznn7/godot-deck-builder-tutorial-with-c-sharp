using Godot;

namespace DeckBuilderTutorialC;

public partial class CardDraggingState : CardState
{
    const float DragMinimumThreshold = 0.05f;
    bool _isMinimumDragTimeElapsed;

    public override void Enter()
    {
        var uiLayer = GetTree().GetFirstNodeInGroup("ui_layer");

        if (uiLayer != null)
        {
            CardUI.Reparent(uiLayer);
        }

        CardUI.ColorRect.Color = new Color(0, 0, 155);
        CardUI.StateLabel.Text = "DRAGGING";

        _isMinimumDragTimeElapsed = false;
        GetTree().CreateTimer(DragMinimumThreshold, false)
            .Timeout += () => { _isMinimumDragTimeElapsed = true; };
    }

    public override void Exit()
    {
    }

    public override void OnInput(InputEvent @event)
    {
        var isMouseMotion = @event is InputEventMouseMotion;
        var isCancel = @event.IsActionPressed("right_mouse");
        var isConfirm = @event.IsActionReleased("left_mouse") || @event.IsActionPressed("left_mouse");
        var isSingleTargeted = CardUI.Card.IsSingleTargeted();

        if (isSingleTargeted && isMouseMotion && CardUI.Targets.Count > 0)
        {
            EmitSignal(CardState.SignalName.TransitionRequested, (int)EState.Dragging, (int)EState.Aiming);
            return;
        }

        if (isMouseMotion)
        {
            CardUI.GlobalPosition = CardUI.GetGlobalMousePosition() - CardUI.PivotOffset;
        }

        if (isCancel)
        {
            EmitSignal(CardState.SignalName.TransitionRequested, (int)EState.Dragging, (int)EState.Base);
            return;
        }
        
        if (isConfirm && _isMinimumDragTimeElapsed)
        {
            GetViewport().SetInputAsHandled();
            EmitSignal(CardState.SignalName.TransitionRequested, (int)EState.Dragging, (int)EState.Released);
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
