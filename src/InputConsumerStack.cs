/* Author:  Leonardo Trevisan Silio
 * Date:    13/02/2024
 */
using System;
using System.Collections.Generic;

using Radiance;

namespace FromSky;

/// <summary>
/// Represents a layer that recive inputs.
/// </summary>
public class InputConsumerStack : LinkedList<Action<ControlData>>
{
    /// <summary>
    /// Add a input consumer layer in start of layers.
    /// </summary>
    public InputConsumerStack Push(Action<ControlData> data)
    {
        AddFirst(data);
        return this;
    }

    /// <summary>
    /// Add a input consumer layer in start of layers.
    /// </summary>
    public InputConsumerStack Push(Func<Action<ControlData>> closure)
    {
        AddFirst(closure());
        return this;
    }

    /// <summary>
    /// Add a input consumer layer in start of layers.
    /// </summary>
    public InputConsumerStack Push(Input input, Action action)
    {
        AddFirst(x =>
        {
            if (x.Contains(input))
                action();
        });
        return this;
    }
    
    /// <summary>
    /// Add a input consumer layer in start of layers.
    /// </summary>
    public InputConsumerStack Push(Input input, Action down, Action up)
    {
        Push(() => {
            bool isdown = false;
            return x =>
            {
                var hasInput = x.Contains(input);
                if (hasInput && !isdown)
                {
                    down();
                    isdown = true;
                }

                if (!hasInput && isdown)
                {
                    up();
                    isdown = false;
                }
            };
        });
        return this;
    }

    public virtual void Consume(ControlData data)
    {
        var crr = First;
        while (crr != null)
        {
            var layer = crr.Value;
            layer?.Invoke(data);

            if (data.RequestPopConsumer)
                Remove(crr);
            
            crr = crr.Next;

            if (!data.HasData)
                break;
        }
    }
}