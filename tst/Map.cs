using FromSky;
using FromSky.Meshes;

using static Radiance.Utils;

public class Map : Scene
{
    public Map()
    {
        for (int i = 0; i < 25; i++)
        {
            for (int j = 0; j < 25; j++)
            {   
                Add(new Cube(blue, 20, 10, 20) { 
                    Position = (-10 * i, 0, -10 * j) 
                });
            }
        }
        Move(1200, 0, 0);
    }
}