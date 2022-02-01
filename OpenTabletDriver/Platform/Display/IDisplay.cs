using System.Numerics;

#nullable enable

namespace OpenTabletDriver.Platform.Display
{
    public interface IDisplay
    {
        int Index { get; }
        float Width { get; }
        float Height { get; }
        Vector2 Position { get; }
    }
}