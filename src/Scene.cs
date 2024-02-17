/* Author:  Leonardo Trevisan Silio
 * Date:    16/02/2024
 */
using System;

namespace FromSky;

using static Radiance.Utils;

public class Scene : Layer
{
    dynamic mainRender;

    public Scene()
    {
        updateAngles();

        mainRender = render(() => {
            clear((0, 0, .4f, 1f));
        });
    }

    public override void Draw()
    {
        mainRender(Empty);
    }

    const float a0 = 35.26f * MathF.PI / 180f;
    const float b0 = 45f * MathF.PI / 180f;

    private float a = a0;
    private float b = b0;
    private float cosa = float.NaN;
    private float cosb = float.NaN;
    private float sina = float.NaN;
    private float sinb = float.NaN;
    private float sinbsina = float.NaN;
    private float cosbsina = float.NaN;

    private void updateAngles()
    {
        cosa = MathF.Cos(a);
        cosb = MathF.Cos(b);
        sina = MathF.Sin(a);
        sinb = MathF.Sin(b);
        sinbsina = sinb * sina;
        cosbsina = cosb * sina;
    }

    private (float x, float y) camTransform(float x, float y, float z)
        => (
            x * cosb - z * sinb,
            y * cosa + x * sinbsina + z * cosbsina
        );
}