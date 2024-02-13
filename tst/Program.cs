using System;

using FromSky;
using Radiance;

Game<TestGame>.New();
var data = Game<TestGame>.Current.Data;

Game.Inputs.Push(Input.S, null, async () => await Game<TestGame>.Save());
Game.Inputs.Push(Input.A, null, () => data.Points++);
Game.Inputs.Push(Input.Escape, null, Game.Close);

Game.Open();

public class TestGame : GameData
{
    [SaveResume]
    public int Points { get; set; }
}