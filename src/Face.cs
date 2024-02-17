/* Author:  Leonardo Trevisan Silio
 * Date:    17/02/2024
 */
using Radiance.Data;

namespace FromSky;

/// <summary>
/// Represents a 3D Face.
/// </summary>
public class Face
{
    private Polygon polygon;
    private float[] rot = [
        1, 0, 0,
        0, 1, 0,
        0, 0, 1
    ];

    public Face(Vec3[] pts)
    {
        polygon = new Polygon();
        foreach (var pt in pts)
            polygon.Add(pt.X, pt.Y, pt.Z);
    }
    
    public static implicit operator Face(Vec3[] pts)
        => new Face(pts);

    public dynamic GetRender(dynamic baseRender)
        => baseRender.Curry(polygon,
            rot[0], rot[1], rot[2],
            rot[3], rot[4], rot[5],
            rot[6], rot[7], rot[8]
        );
}