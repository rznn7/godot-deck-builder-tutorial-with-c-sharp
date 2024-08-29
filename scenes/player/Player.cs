using DeckBuilderTutorialC.Extensions;
using Godot;

namespace DeckBuilderTutorialC.player;

public partial class Player : Node2D
{
    CharacterStats _stats;

    [Export]
    public CharacterStats Stats
    {
        get { return _stats; }
        set
        {
            _stats = value.CreateInstance();
            Stats.StatsChanged += UpdateStats;
            UpdatePlayer();
        }
    }

    Sprite2D _sprite2D;
    StatsUI _statsUI;

    public override void _Ready()
    {
        _sprite2D = GetNode<Sprite2D>("Sprite2D");
        _statsUI = GetNode<StatsUI>("StatsUI");
        
        UpdatePlayer();
    }

    async void UpdatePlayer()
    {
        if (!IsInsideTree())
        {
            await this.AwaitNodeReady();
        }

        _sprite2D.Texture = Stats.Art;
        UpdateStats();
    }

    void UpdateStats()
    {
        _statsUI.UpdateStats(Stats);
    }

    void TakeDamage(int damage)
    {
        if (Stats.Health <= 0) return;

        Stats.TakeDamage(damage);

        if (Stats.Health <= 0)
        {
            QueueFree();
        }
    }
}
