using DeckBuilderTutorialC.Extensions;
using Godot;

namespace DeckBuilderTutorialC;

public partial class CardBaseState : CardState
{
    public override async void Enter()
    {
        if (!CardUI.IsNodeReady())
        {
            await CardUI.AwaitNodeReady();
        }

        CardUI.Tween?.Kill();

        CardUI.EmitSignal(CardUI.SignalName.ReParentRequested, CardUI);
        CardUI.ColorRect.Color = new Color(0, 128, 0);
        CardUI.StateLabel.Text = "BASE";
        CardUI.PivotOffset = Vector2.Zero;
    }

    public override void Exit()
    {
    }

    public override void OnInput(InputEvent @event)
    {
    }

    public override void OnGUIInput(InputEvent @event)
    {
        if (@event.IsActionPressed("left_mouse"))
        {
            CardUI.PivotOffset = CardUI.GetGlobalMousePosition() - CardUI.GlobalPosition;
            EmitSignal(CardState.SignalName.TransitionRequested, (int)EState.Base, (int)EState.Clicked);
        }
    }

    public override void OnMouseEntered()
    {
    }

    public override void OnMouseExited()
    {
    }
}
