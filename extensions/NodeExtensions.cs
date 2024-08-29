using System.Threading.Tasks;
using Godot;

namespace DeckBuilderTutorialC.Extensions;

public static class NodeExtensions
{
    public static async Task AwaitNodeReady(this Node node)
    {
        if (!node.IsInsideTree())
        {
            var tcs = new TaskCompletionSource<bool>();

            node.Connect("ready", new Callable(node, nameof(OnReady)));
            await tcs.Task;

            void OnReady()
            {
                tcs.SetResult(true);
                node.Disconnect("ready",
                    new Callable(node, nameof(OnReady)));
            }
        }
    }
}
