/* Author:  Leonardo Trevisan Silio
 * Date:    13/02/2024
 */
using Radiance;

namespace FromSky;

/// <summary>
/// Represents a layer that recive inputs.
/// </summary>
public abstract class InputLayer
{
    public InputLayer Next { get; private set; }

    private bool consuming = false;
    public void Consume()
        => consuming = true;

    public void ReciveMouseDown(Input input, Modifier modifier)
    {
        consuming = false;
        
    }
    public virtual void ConsumeMouseDown(Input input, Modifier modifier) { }
}