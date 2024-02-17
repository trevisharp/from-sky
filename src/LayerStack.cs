/* Author:  Leonardo Trevisan Silio
 * Date:    16/02/2024
 */
using System.Collections.Generic;

namespace FromSky;

/// <summary>
/// Represents a stack of layers.
/// </summary>
public class LayerStack : LinkedList<Layer>
{
    /// <summary>
    /// Add a Layer to the top of view.
    /// </summary>
    public LayerStack Push(Layer layer)
    {
        AddLast(layer);
        return this;
    }

    /// <summary>
    /// Remove a Layer to the top of view.
    /// </summary>
    public LayerStack Pop()
    {
        RemoveLast();
        return this;
    }

    /// <summary>
    /// Draw the last layer.
    /// </summary>
    public void Draw()
        => Last.Value?.Draw();
}