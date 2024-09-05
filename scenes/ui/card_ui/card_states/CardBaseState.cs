using DeckBuilderTutorialC.Extensions;
using global::DeckBuilderTutorialC.global;
using Godot;

namespace DeckBuilderTutorialC;

public partial class CardBaseState : CardState
{
    public override async void Enter()
    {
        await CardUI.AwaitNodeReady();

        CardUI.Tween?.Kill();

        CardUI.Panel.AddThemeStyleboxOverride("panel", CardUI.CardBaseStyleBox);
        CardUI.EmitSignal(CardUI.SignalName.ReParentRequested, CardUI);
        CardUI.PivotOffset = Vector2.Zero;

        Events.Instance.EmitSignal(Events.SignalName.CardTooltipHideRequested);
    }

    public override void Exit()
    {
    }

    public override void OnInput(InputEvent @event)
    {
    }

    public override void OnGUIInput(InputEvent @event)
    {
        if (!CardUI.IsPlayable || CardUI.IsDisabled) return;
        if (!@event.IsActionPressed("left_mouse")) return;

        CardUI.PivotOffset = CardUI.GetGlobalMousePosition() - CardUI.GlobalPosition;
        EmitSignal(CardState.SignalName.TransitionRequested, (int)EState.Base, (int)EState.Clicked);
    }

    public override void OnMouseEntered()
    {
        if (!CardUI.IsPlayable || CardUI.IsDisabled) return;

        CardUI.Panel.AddThemeStyleboxOverride("panel", CardUI.CardHoverStyleBox);

        Events.Instance.EmitSignal(Events.SignalName.CardTooltipShowRequested, CardUI.Card);
    }

    public override void OnMouseExited()
    {
        if (!CardUI.IsPlayable || CardUI.IsDisabled) return;

        CardUI.Panel.AddThemeStyleboxOverride("panel", CardUI.CardBaseStyleBox);

        Events.Instance.EmitSignal(Events.SignalName.CardTooltipHideRequested);
    }
}
