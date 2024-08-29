using System;
using global::DeckBuilderTutorialC.global;
using Godot;

namespace DeckBuilderTutorialC;

public partial class CardAimingState : CardState
{
    const float MouseYSnapBackThreshold = 138f;

    public override void Enter()
    {
        CardUI.ColorRect.Color = new Color(255f, 255f, 0f);
        CardUI.StateLabel.Text = "AIMING";
        CardUI.Targets.Clear();

        var offset = new Vector2(CardUI.Parent.Size.X / 2, -CardUI.Size.Y / 2);
        offset.X -= CardUI.Size.X / 2;
        CardUI.AnimateToPosition(CardUI.Parent.GlobalPosition + offset, 0.2f);
        CardUI.DropPointDetector.Monitoring = false;

        Events.Instance.EmitSignal(Events.SignalName.CardAimStarted, CardUI);
    }

    public override void Exit()
    {
        Events.Instance.EmitSignal(Events.SignalName.CardAimEnded, CardUI);
    }

    public override void OnInput(InputEvent @event)
    {
        var isMouseMotion = @event is InputEventMouseMotion;
        var isMouseAtBottom = CardUI.GetGlobalMousePosition().Y > MouseYSnapBackThreshold;

        if (isMouseMotion && isMouseAtBottom || @event.IsActionPressed("right_mouse"))
        {
            EmitSignal(CardState.SignalName.TransitionRequested, (int)EState.Aiming, (int)EState.Base);
        }
        else if (@event.IsActionReleased("left_mouse") || @event.IsActionPressed("left_mouse"))
        {
            GetViewport().SetInputAsHandled();
            EmitSignal(CardState.SignalName.TransitionRequested, (int)EState.Aiming, (int)EState.Released);
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
