using System;

using FromSky;
using FromSky.Meshes;

using Radiance;
using static Radiance.Utils;

var save = new SaveSystem<TestGame>();
save.New();

Scene world =
[
    new Cube(blue, 50, 50, 50),
];

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