/* Author:  Leonardo Trevisan Silio
 * Date:    17/02/2024
 */
using Radiance.Data;

namespace FromSky;

/// <summary>
/// Represents a complex 3D object
/// </summary>
public abstract class Mesh
{
    public Vec3 Position { get; set; } = (0, 0, 0);

    public abstract void Draw();
}