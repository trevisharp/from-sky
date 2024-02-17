/* Author:  Leonardo Trevisan Silio
 * Date:    17/02/2024
 */
using System.Collections.Generic;

using Radiance.Data;
using static Radiance.Utils;

namespace FromSky.Meshes;

public class Cube : Mesh
{
    Vec4 color;
    List<Face> faces = [];
    void add(Vec3[] pts)
        => faces.Add(pts);

    public Cube(Vec4 color, float sx, float sy, float sz)
    {
        this.color = color;
        sx /= 2;
        sy /= 2;
        sz /= 2;

        Position = (300, 300, 0);

        add([ (-sx, -sy, -sz), (+sx, -sy, -sz), (+sx, +sy, -sz), (-sx, +sy, -sz) ]);
        add([ (-sx, +sy, -sz), (-sx, +sy, +sz), (+sx, +sy, +sz), (+sx, +sy, -sz) ]);
        add([ (-sx, -sy, -sz), (-sx, -sy, +sz), (-sx, +sy, +sz), (-sx, +sy, -sz) ]);
    }

    public override void Draw()
    {
        faces[0].GetRender(Renders.Fill)(
            Position, color
        );
        faces[1].GetRender(Renders.Fill)(
            Position, color
        );
        faces[2].GetRender(Renders.Fill)(
            Position, color
        );
    }
}