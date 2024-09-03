using Godot;
using Godot.Collections;

namespace DeckBuilderTutorialC;

public partial class DamageEffect : Effect
{
    public int Amount;

    public override void Execute(Array<Node> targets)
    {
        foreach (var target in targets)
        {
            switch (target)
            {
                case Enemy enemy:
                    enemy.TakeDamage(Amount);
                    break;
                case Player player:
                    player.TakeDamage(Amount);
                    break;
            }
        }
    }
}
