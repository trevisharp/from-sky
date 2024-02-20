using System;

using FromSky;
using FromSky.Meshes;

using Radiance;
using static Radiance.Utils;

var save = new SaveSystem<TestGame>();
save.New();

Scene world = new Map();

Game.Inputs.Push(Input.W, null, () => world.Move(10, 0, 0));
Game.Inputs.Push(Input.A, null, () => world.Move(0, 0, 10));
Game.Inputs.Push(Input.S, null, () => world.Move(-10, 0, 0));
Game.Inputs.Push(Input.D, null, () => world.Move(0, 0, -10));
Game.Inputs.Push(Input.Escape, null, Game.Close);

Game.Layers.Push(world);

Game.Open();

public class TestGame : SaveData
{
    [SaveResume]
    public int Points { get; set; }
}