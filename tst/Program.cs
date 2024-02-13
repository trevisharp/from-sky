using System;

using FromSky;
using Radiance;

Game<TestGame>.New();


Game.Open();

public class TestGame : GameData
{
    [SaveResume]
    public int Points { get; set; }
}