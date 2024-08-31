using DeckBuilderTutorialC.Extensions;
using Godot;

namespace DeckBuilderTutorialC.enemy;

public partial class Enemy : Area2D
{
    const float ArrowOffset = 5f;

    Stats _stats;

    [Export]
    public Stats Stats
    {
        get { return _stats; }
        set
        {
            _stats = value.CreateInstance();
            _stats.StatsChanged += UpdateStats;
            UpdateEnemy();
        }
    }

    Sprite2D _sprite2D;
    Sprite2D _arrow;
    StatsUI _statsUi;

    public override void _Ready()
    {
        _sprite2D = GetNode<Sprite2D>("Sprite2D");
        _arrow = GetNode<Sprite2D>("Arrow");
        _statsUi = GetNode<StatsUI>("StatsUI");

        UpdateEnemy();
    }

    public override void _Process(double delta)
    {
    }

    async void UpdateEnemy()
    {
        await this.AwaitNodeReady();

        _sprite2D.Texture = _stats.Art;
        _arrow.Position = Vector2.Right * (_sprite2D.GetRect().Size.X / 2 + ArrowOffset);
        UpdateStats();
    }

    void UpdateStats()
    {
        _statsUi.UpdateStats(_stats);
    }

    void TakeDamage(int damage)
    {
        if (_stats.Health <= 0) return;

        _stats.TakeDamage(damage);

        if (_stats.Health <= 0)
        {
            QueueFree();
        }
    }
}
