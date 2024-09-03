using Godot;

namespace DeckBuilderTutorialC;

public partial class ManaUI : Panel
{
    [Export]
    public CharacterStats CharacterStats
    {
        get { return _characterStats; }
        set
        {
            _characterStats = value;
            _characterStats.StatsChanged += UpdateManaUI;
            if (IsNodeReady()) UpdateManaUI();
        }
    }

    CharacterStats _characterStats;
    
    // @export variables
    Label _manaLabel;

    public override void _Ready()
    {
        _manaLabel = GetNode<Label>("ManaLabel");
        CharacterStats.Mana = 2;

        UpdateManaUI();
    }

    void UpdateManaUI()
    {
        _manaLabel.Text = $"{CharacterStats.Mana}/{CharacterStats.MaxMana}";
    }
}
