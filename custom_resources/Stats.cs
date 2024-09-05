using DeckBuilderTutorialC.Extensions;
using Godot;

namespace DeckBuilderTutorialC;

[GlobalClass]
public partial class Stats : Resource
{
    [Signal]
    public delegate void StatsChangedEventHandler();

    [Export]
    public int MaxHealth = 1;

    [Export]
    public Texture2D Art;

    int _health;

    public int Health
    {
        get { return _health; }
        set { SetHealth(value); }
    }

    int _block;

    public int Block
    {
        get { return _block; }
        set { SetBlock(value); }
    }

    const int MaxBlock = 999;

    void SetHealth(int value)
    {
        _health = Mathf.Clamp(value, 0, MaxHealth);
        EmitSignal(SignalName.StatsChanged);
    }

    void SetBlock(int value)
    {
        _block = Mathf.Clamp(value, 0, MaxBlock);
        EmitSignal(SignalName.StatsChanged);
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0) return;

        var damageTaken = Mathf.Clamp(damage - _block, 0, damage);
        Block -= damage;
        Health -= damageTaken;
    }

    public void Heal(int heal)
    {
        Health += heal;
    }

    public virtual Stats CreateInstance()
    {
        var instance = this.Duplicate<Stats>();
        instance.Health = MaxHealth;
        instance.Block = 0;

        return instance;
    }
}
