/* Author:  Leonardo Trevisan Silio
 * Date:    17/02/2024
 */
using System;

using Radiance.Renders;
using Radiance.Shaders.Objects;
using static Radiance.Utils;

namespace FromSky;

public class Renders
{
    private static dynamic _fill = null;
    public static dynamic Fill
    {
        get
        {
            if (_fill is not null)
                return _fill;
            
            _fill = new Render((
                FloatShaderObject m00, FloatShaderObject m01, FloatShaderObject m02,
                FloatShaderObject m10, FloatShaderObject m11, FloatShaderObject m12,
                FloatShaderObject m20, FloatShaderObject m21, FloatShaderObject m22,
                FloatShaderObject bx, FloatShaderObject by, FloatShaderObject bz,
                FloatShaderObject r,
                FloatShaderObject g, 
                FloatShaderObject b,
                FloatShaderObject a
            ) =>
            {
                var xl = pos.x * m00 + pos.y * m01 + pos.z * m02 + bx;
                var yl = pos.x * m10 + pos.y * m11 + pos.z * m12 + by;
                var zl = pos.x * m20 + pos.y * m21 + pos.z * m22 + bz;

                pos = (
                    (xl - zl) / MathF.Sqrt(2),
                    (xl + 2 * yl + zl) / MathF.Sqrt(6),
                    0
                );
                color = (r, g, b, a);
                fill();
            });
            return _fill;
        }
    }
    
    private static dynamic _draw = null;
    public static dynamic Draw
    {
        get
        {
            if (_draw is not null)
                return _draw;
            
            _draw = new Render((
                FloatShaderObject m00, FloatShaderObject m01, FloatShaderObject m02,
                FloatShaderObject m10, FloatShaderObject m11, FloatShaderObject m12,
                FloatShaderObject m20, FloatShaderObject m21, FloatShaderObject m22,
                FloatShaderObject bx, FloatShaderObject by, FloatShaderObject bz,
                FloatShaderObject r,
                FloatShaderObject g, 
                FloatShaderObject b,
                FloatShaderObject a
            ) =>
            {
                var xl = pos.x * m00 + pos.y * m01 + pos.z * m02 + bx;
                var yl = pos.x * m10 + pos.y * m11 + pos.z * m12 + by;
                var zl = pos.x * m20 + pos.y * m21 + pos.z * m22 + bz;

                pos = (
                    (xl - zl) / MathF.Sqrt(2),
                    (xl + 2 * yl + zl) / MathF.Sqrt(6),
                    0
                );
                color = (r, g, b, a);
                draw();
            });
            return _draw;
        }
    }
}