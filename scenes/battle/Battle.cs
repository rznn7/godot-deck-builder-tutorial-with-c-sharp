using DeckBuilderTutorialC.global;
using Godot;

namespace DeckBuilderTutorialC.battle;

public partial class Battle : Node2D
{
    [Export]
    CharacterStats CharacterStats { get; set; }

    // @onready variables
    BattleUi _battleUi;
    PlayerHandler _playerHandler;
    Player _player;

    public override void _Ready()
    {
        _battleUi = GetNode<BattleUi>("BattleUI");
        _playerHandler = GetNode<PlayerHandler>("PlayerHandler");
        _player = GetNode<Player>("Player");

        // Normally we would do this on a 'Run' level so we keep our stats between battles
        var newCharStats = CharacterStats.CreateInstance();
        _battleUi.CharStats = newCharStats;
        _player.Stats = newCharStats;

        Events.Instance.PlayerHandDiscarded += _playerHandler.StartTurn;
        Events.Instance.PlayerTurnEnded += _playerHandler.EndTurn;

        StartBattle(newCharStats);
    }

    void StartBattle(CharacterStats charStats)
    {
        _playerHandler.StartBattle(charStats);
    }
}
