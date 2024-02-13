/* Author:  Leonardo Trevisan Silio
 * Date:    13/02/2024
 */
using System.Collections.Generic;

using Radiance;

namespace FromSky;

/// <summary>
/// All data of inputs.
/// </summary>
public class ControlData
{
    internal bool HasData { get; private set; } = true;
    internal bool RequestPopConsumer { get; private set; } = false;

    public HashSet<Input> Inputs { get; set; }
    public HashSet<MouseButton> MouseButtons { get; set; }
    public float? CursorX { get; set; }
    public float? CursorY { get; set; }
    public float? Whell { get; set; }
    public bool? MouseInScreen { get; set; }

    public bool Contains(Input data)
    {
        if (Inputs is null)
            return false;
        
        return Inputs.Contains(data);
    }
    
    public bool Contains(MouseButton data)
    {
        if (MouseButtons is null)
            return false;
        
        return MouseButtons.Contains(data);
    }

    public void Consume(Input data)
        => Inputs?.Remove(data);
    
    public void Consume(MouseButton data)
        => MouseButtons?.Remove(data);
    
    public void Consume(HashSet<Input> data)
    {
        if (Inputs == data)
            Inputs = null;
    }
    
    public void Consume(HashSet<MouseButton> data)
    {
        if (MouseButtons == data)
            MouseButtons = null;
    }
    
    /// <summary>
    /// Consume all data stopping the data consumition.
    /// </summary>
    public void Consume()
        => HasData = false;

    /// <summary>
    /// Remove this consumer from consumers stack.
    /// </summary>
    public void Pop()
        => RequestPopConsumer = true;
}