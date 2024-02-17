/* Author:  Leonardo Trevisan Silio
 * Date:    16/02/2024
 */
using Radiance;

namespace FromSky;

/// <summary>
/// Control the game input and outputs.
/// </summary>
public static class Game
{
    private static LayerStack layers = [];
    public static LayerStack Layers => layers;

    private static InputConsumerStack inputStack = [];
    public static InputConsumerStack Inputs => inputStack;

    private static ControlManager manager = new ControlManager();

    private static bool isLoaded = false;
    public static void Open()
    {
        if (Window.IsOpen)
            return;
        
        if (isLoaded)
        {
            Window.Open();
            return;
        }
        isLoaded = true;

        Window.OnKeyDown += manager.ReciveKeyDown;
        Window.OnKeyUp += manager.ReciveKeyUp;
        Window.OnMouseMove += manager.ReciveMouseMove;
        Window.OnMouseEnter += manager.ReciveMouseEnter;
        Window.OnMouseLeave += manager.ReciveMouseLeave;
        Window.OnMouseDown += manager.ReciveMouseDown;
        Window.OnMouseUp += manager.ReciveMouseUp;
        Window.OnMouseWhell += manager.ReciveMouseWheel;
        Window.OnFrame += manager.ReciveFrame;
        Window.OnRender += layers.Draw;

        Window.OnFrame += () => 
            Inputs.Consume(
                manager.GetData()
            );

        Window.OnLoad += () =>
        {

        };

        Window.OnUnload += () =>
        {

        };
    

        Window.Open();
    }

    public static void Close()
        => Window.Close();
}