using Godot;

namespace DeckBuilderTutorialC;

public partial class CardReleaseState : CardState
{
    bool _played;

    public override void Enter()
    {
        _played = false;

        if (CardUI.Targets.Count > 0)
        {
            _played = true;
            GD.Print($"play card for target.s {CardUI.Targets}");
            CardUI.Play();
        }
    }

    public override void Exit()
    {
    }

    public override void OnInput(InputEvent @event)
    {
        if (_played) return;

        EmitSignal(CardState.SignalName.TransitionRequested, (int)EState.Released, (int)EState.Base);
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
