using Godot;
using Godot.Collections;

namespace DeckBuilderTutorialC;

public partial class WarriorBlock : Card
{
    protected override void ApplyEffect(Array<Node> targets)
    {
        var blockEffect = new BlockEffect();
        blockEffect.Amount = 5;
        blockEffect.Execute(targets);
    }
}
