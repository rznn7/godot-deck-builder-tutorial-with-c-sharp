using Godot;

namespace DeckBuilderTutorialC.Extensions;

public static class ResourceExtensions
{
    public static T Duplicate<T>(this Resource resource, bool subresource = false) where T : Resource
    {
        return (T)resource.Duplicate(subresource);
    }
}
