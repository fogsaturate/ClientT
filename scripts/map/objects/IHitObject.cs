using Godot;
using System;

/// <summary>
/// Represents a interactable object inside the map
/// </summary>
public interface IHitObject : ITimelineObject
{
    /// <summary>
    /// X position of the <see cref="IHitObject"/>
    /// </summary>
    float X { get; set; }

    /// <summary>
    /// Y position of the <see cref="IHitObject"/>
    /// </summary>
    float Y { get; set; }

    ///// <summary>
    ///// Hit window for the <see cref="IHitObject"/>
    ///// </summary>
    //int HitWindow { get; }
}
