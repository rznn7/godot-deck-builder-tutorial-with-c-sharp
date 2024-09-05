using global::DeckBuilderTutorialC.global;
using Godot;

namespace DeckBuilderTutorialC;

public partial class BattleUi : CanvasLayer
{
    [Export]
    public CharacterStats CharStats
    {
        get { return _charStats; }
        set { SetCharStats(value); }
    }

    CharacterStats _charStats;

    // @onready variables
    Hand _hand;
    ManaUI _manaUI;
    Button _endTurnButton;
    
    void SetCharStats(CharacterStats value)
    {
        _charStats = value;
        _manaUI.CharacterStats = _charStats;
        _hand.CharacterStats = _charStats;
    }

    public override void _Ready()
    {
        _hand = GetNode<Hand>("Hand");
        _manaUI = GetNode<ManaUI>("ManaUI");
        _endTurnButton = GetNode<Button>("EndTurnButton");

        Events.Instance.PlayerHandDrawn += OnPlayerHandDrawn;
        _endTurnButton.Pressed += OnEndTurnButtonPressed;
    }

    void OnPlayerHandDrawn()
    {
        _endTurnButton.Disabled = false;
    }

    void OnEndTurnButtonPressed()
    {
        _endTurnButton.Disabled = true;
        Events.Instance.EmitSignal(Events.SignalName.PlayerTurnEnded);
    }
}
