/* Author:  Leonardo Trevisan Silio
 * Date:    13/02/2024
 */
using System;
using System.Collections.Generic;

using Radiance;

namespace FromSky;

/// <summary>
/// Control the game input over time.
/// </summary>
public class ControlManager
{
    public HashSet<Input> Inputs { get; private set; } = [];
    public HashSet<MouseButton> MouseButtons { get; private set; } = [];
    public float CursorX { get; private set; }
    public float CursorY { get; private set; }
    public float Whell { get; private set; }
    public bool MouseInScreen { get; private set; } = false;

    public virtual void ReciveKeyDown(Input input, Modifier modifier)
        => Inputs.Add(input);

    public virtual void ReciveKeyUp(Input input, Modifier modifier)
        => Inputs.Remove(input);

    public virtual void ReciveMouseMove((float x, float y) p)
    {
        CursorX = p.x;
        CursorY = p.y;
    }

    public virtual void ReciveMouseEnter()
        => MouseInScreen = true;

    public virtual void ReciveMouseLeave()
        => MouseInScreen = false;
    
    public virtual void ReciveMouseDown(MouseButton button)
        => MouseButtons.Add(button);
    
    public virtual void ReciveMouseUp(MouseButton button)
        => MouseButtons.Remove(button);
    
    private float currentWhell = 0;
    private float lastWhell = 0;
    public virtual void ReciveMouseWheel(float whell)
        => currentWhell += whell;
    
    public virtual void ReciveFrame()
    {
        Whell = currentWhell - lastWhell;
        lastWhell = currentWhell;
    }
}