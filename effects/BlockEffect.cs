using Godot;
using Godot.Collections;

namespace DeckBuilderTutorialC;

public partial class BlockEffect : Effect
{
    public int Amount;

    public override void Execute(Array<Node> targets)
    {
        foreach (var target in targets)
        {
            switch (target)
            {
                case Enemy enemy:
                    enemy.Stats.Block += Amount;
                    break;
                case Player player:
                    player.Stats.Block += Amount;
                    break;
            }
        }
    }
}
