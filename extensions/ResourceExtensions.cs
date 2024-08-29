using Godot;

namespace DeckBuilderTutorialC.Extensions;

public static class ResourceExtensions
{
    public static T DuplicateAs<T>(this Resource resource) where T : Resource
    {
        return (T)resource.Duplicate();
    }
}
