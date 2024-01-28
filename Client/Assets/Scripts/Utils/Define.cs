using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum MouseEvent
    {
        PointerDown,
        Press,
        Click,
        PointerUp
    }

    public enum MoveDir
    {
        None,
        Idle,
        Up,
        Down,
        Left,
        Right,
    }

    public enum Scene
    {
        Unknown,
        Login,
        Lobby, // Character º±≈√√¢
        Game,
    }

    public enum Sound
    {
        Bgm,
        Effect,

        MaxCount,
    }

    public enum UIEvent
    {
        Click,
        Drag,
    }
}
