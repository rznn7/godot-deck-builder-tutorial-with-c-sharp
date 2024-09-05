using DeckBuilderTutorialC.global;
using Godot;
using Godot.Collections;

namespace DeckBuilderTutorialC;

[GlobalClass]
public abstract partial class Card : Resource
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
    EType _type;

    [Export]
    ETarget _target;

    [Export]
    public int Cost;

    [ExportGroup("Card Visuals")]
    [Export]
    public Texture2D Icon;

    [Export(PropertyHint.MultilineText)]
    public string TooltipText;

    public bool IsSingleTargeted()
    {
        return _target == ETarget.SingleEnemy;
    }

    Array<Node> GetTargets(Array<Node> targets)
    {
        if (targets == null) return new Array<Node>();

        var tree = targets[0].GetTree();

        return _target switch
        {
            ETarget.Self => tree.GetNodesInGroup("player"),
            ETarget.AllEnemies => tree.GetNodesInGroup("enemies"),
            ETarget.Everyone => tree.GetNodesInGroup("player") + tree.GetNodesInGroup("enemies"),
            _ => new Array<Node>()
        };
    }

    public void Play(Array<Node> targets, CharacterStats characterStats)
    {
        Events.Instance.EmitSignal(Events.SignalName.CardPlayed, this);
        characterStats.Mana -= Cost;

        if (IsSingleTargeted())
        {
            ApplyEffect(targets);
        }
        else
        {
            ApplyEffect(GetTargets(targets));
        }
    }

    protected abstract void ApplyEffect(Array<Node> targets);
}
