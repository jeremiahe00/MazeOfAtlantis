
public static class Constants
{
    public static readonly string PlayerTag = "Player";
    //public static readonly string AnimationStarted = "started";
    //public static readonly string AnimationPropel = "propel";

    public static readonly string StatusClickToStart = "Click to start";
    public static readonly string StatusDeadClickToStart = "Dead. Click to start";
}

public enum GameState
{
    Start,
    Playing,
    Dead
}