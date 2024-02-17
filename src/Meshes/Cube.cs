/* Author:  Leonardo Trevisan Silio
 * Date:    17/02/2024
 */
using Radiance.Data;
using static Radiance.Utils;

namespace FromSky.Meshes;

public class Cube : Mesh
{
    public Cube(Vec4 color, float sx, float sy, float sz)
    {
        Add((
            [ (0, 0, 0), (sx, 0, 0), (sx, sy, 0), (0, sy, 0) ],
            Kit.Fill.Curry(color.X * .8f, color.Y * .8f, color.Z * .8f, color.W)
        ));

        // Add((
        //     [ (0, 0, sz), (sx, 0, sz), (sx, sy, sz), (0, sy, sz) ],
        //     Kit.Fill
        // ));

        Add((
            [ (0, 0, 0), (0, 0, sz), (sx, 0, sz), (sx, 0, 0) ],
            Kit.Fill.Curry(color.X, color.Y, color.Z, color.W)
        ));

        // Add((
        //     [ (0, sy, 0), (0, sy, sz), (sx, sy, sz), (sx, sy, 0) ],
        //     Kit.Fill
        // ));

        Add((
            [ (0, 0, 0), (0, 0, sz), (0, sy, sz), (0, sy, 0) ],
            Kit.Fill.Curry(color.X * .6f, color.Y * .6f, color.Z * .6f, color.W)
        ));

        // Add((
        //     [ (sx, 0, 0), (sx, 0, sz), (sx, sy, sz), (sx, sy, 0) ],
        //     Kit.Fill
        // ));
    }
}