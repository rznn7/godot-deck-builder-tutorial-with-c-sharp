using Godot.Collections;

namespace DeckBuilderTutorialC.Extensions;

public static class ArrayExtensions
{
    public static Card PopFront(this Array<Card> cards)
    {
        if (cards.Count == 0)
        {
            return null;
        }

        var firstCard = cards[0];
        cards.RemoveAt(0);
        return firstCard;
    }
}
