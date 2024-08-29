using Godot;

namespace DeckBuilderTutorialC;

[GlobalClass]
public partial class Card : Resource
{
    public enum EType
    {
        Attack,
        Skill,
        Power
    }

    public enum ETarget
    {
        Self,
        SingleEnemy,
        AllEnemies,
        Everyone
    }

    [ExportGroup("Card Attributes")]
    [Export]
    public string Id;

    [Export]
    public EType Type;

    [Export]
    public ETarget Target;

    [Export]
    public int Cost;

    public bool IsSingleTargeted()
    {
        return Target == ETarget.SingleEnemy;
    }
}
