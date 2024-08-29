using Godot;

namespace DeckBuilderTutorialC;

public partial class StatsUI : HBoxContainer
{
    HBoxContainer _healthContainer;
    HBoxContainer _blockContainer;
    Label _healthLabel;
    Label _blockLabel;

    public override void _Ready()
    {
        _healthContainer = GetNode<HBoxContainer>("HealthContainer");
        _blockContainer = GetNode<HBoxContainer>("BlockContainer");
        _healthLabel = GetNode<Label>("HealthContainer/HealthLabel");
        _blockLabel = GetNode<Label>("BlockContainer/BlockLabel");
    }

    public void UpdateStats(Stats stats)
    {
        _healthLabel.Text = $"{stats.Health}";
        _blockLabel.Text = $"{stats.Block}";

        _healthContainer.Visible = stats.Health > 0;
        _blockContainer.Visible = stats.Block > 0;
    }
}
