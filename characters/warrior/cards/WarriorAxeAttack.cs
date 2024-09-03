using Godot;
using Godot.Collections;

namespace DeckBuilderTutorialC;

public partial class WarriorAxeAttack : Card
{
    protected override void ApplyEffect(Array<Node> targets)
    {
        var damageEffect = new DamageEffect();
        damageEffect.Amount = 6;
        damageEffect.Execute(targets);
    }
}
