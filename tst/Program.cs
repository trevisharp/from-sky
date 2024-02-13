using System;

using FromSky;
using Radiance;

Game<TestGame>.New();

Game.Inputs.Push(Input.S, null, async () => await Game<TestGame>.Save());
Game.Inputs.Push(Input.A, null, () => Game<TestGame>.Current.Data.Points++);
Game.Inputs.Push(Input.Escape, null, Game.Close);

Game.Open();

public class TestGame : GameData
{
    [SaveResume]
    public int Points { get; set; }
}