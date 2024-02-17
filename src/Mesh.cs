/* Author:  Leonardo Trevisan Silio
 * Date:    17/02/2024
 */
using System;
using System.Collections.Generic;

namespace FromSky;

public class Mesh : List<Face>
{
    public void Draw()
    {
        foreach (var face in this)
            face.Draw();
    }

    public void SetCamTransform(
        Func<float, float, float, (float x, float y)> camTransform
    )
    {
        foreach (var face in this)
            face.SetCamTransform(camTransform);
    }
}