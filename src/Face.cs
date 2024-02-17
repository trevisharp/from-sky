/* Author:  Leonardo Trevisan Silio
 * Date:    17/02/2024
 */
using System;
using System.Collections.Generic;

using Radiance.Data;

namespace FromSky;

/// <summary>
/// Represents a 3D Face.
/// </summary>
public class Face
{
    public Face(List<Vec3> pts, dynamic render)
    {
        this.render = render;
        this.points = pts;
        updatePoints();
    }

    public static implicit operator Face((List<Vec3> pts, dynamic render) tuple)
        => new Face(tuple.pts, tuple.render);

    public void Draw()
        => render(polygon);

    public void MoveTo(Vec3 newPosition)
    {
        position = newPosition;
        updatePoints();
    }

    public void SetCamTransform(Func<float, float, float, (float x, float y)> camTransform)
    {
        this.camTransform = camTransform;
        updatePoints();
    }

    private Func<float, float, float, (float x, float y)> camTransform;
    private dynamic render;
    private Polygon polygon;
    private Vec3 position = (0f, 0f, 0f);
    private List<Vec3> points;
    private float[] rot = [
        1, 0, 0,
        0, 1, 0,
        0, 0, 1
    ];

    private void updatePoints()
    {
        if (camTransform is null)
            return;

        polygon = new Polygon();   
        int len = points.Count;
        for (int i = 0; i < len; i++)
        {
            (float x, float y, float z) = points[i];
            float ox = rot[0] * x + rot[1] * y + rot[2] * z;
            float oy = rot[3] * x + rot[4] * y + rot[5] * z;
            float oz = rot[6] * x + rot[7] * y + rot[8] * z;
            var finalPos = camTransform(
                position.X + ox,
                position.Y + oy,
                position.Z + oz
            );
            polygon.Add(finalPos.x, finalPos.y, 0f);
        }
    }
}