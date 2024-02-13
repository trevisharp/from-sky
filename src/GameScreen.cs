/* Author:  Leonardo Trevisan Silio
 * Date:    13/02/2024
 */
using System;
using System.Collections.Generic;

using Radiance;

namespace FromSky;

/// <summary>
/// Control the game input and outputs.
/// </summary>
public static class Game
{
    private static List<Layer> layers = [];
    public static IEnumerable<Layer> Layers => layers;

    private static ControlManager manager = new ControlManager();

    private static bool isOpened = false;
    public static void Open()
    {
        if (isOpened)
            return;
        isOpened = true;

        Window.OnKeyDown += manager.ReciveKeyDown;
        Window.OnKeyUp += manager.ReciveKeyUp;
        Window.OnMouseMove += manager.ReciveMouseMove;
        Window.OnMouseEnter += manager.ReciveMouseEnter;
        Window.OnMouseLeave += manager.ReciveMouseLeave;
        Window.OnMouseDown += manager.ReciveMouseDown;
        Window.OnMouseUp += manager.ReciveMouseUp;
        Window.OnMouseWhell += manager.ReciveMouseWheel;
        Window.OnFrame += manager.ReciveFrame;

        Window.OnLoad += () =>
        {

        };

        Window.OnUnload += () =>
        {

        };
    
        Window.OnRender += delegate
        {
            foreach (var layer in layers)
                layer.Draw();
        };

        Window.Open(false);
    }
}