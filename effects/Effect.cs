using Godot;
using Godot.Collections;

namespace DeckBuilderTutorialC;

public abstract partial class Effect : RefCounted
{
    public abstract void Execute(Array<Node> targets);
}