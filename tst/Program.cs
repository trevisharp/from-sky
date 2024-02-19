using System;

using FromSky;
using FromSky.Meshes;

using Radiance;
using static Radiance.Utils;

var save = new SaveSystem<TestGame>();
save.New();

Scene world = [];

for (int i = 0; i < 20; i++)
    for (int j = 0; j < 20; j++)
        world.Add(new Cube(blue, 50, 50, 50) { 
            Position = (1500 - 50 * i, 0, 500 - 50 * j) 
        });

Game.Inputs.Push(Input.S, null, async () => await save.Save());
Game.Inputs.Push(Input.A, null, () => save.Current.Points++);
Game.Inputs.Push(Input.Escape, null, Game.Close);

Game.Layers.Push(world);

Game.Open();

public class TestGame : SaveData
{
    [SaveResume]
    public int Points { get; set; }
}