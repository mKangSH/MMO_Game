using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum WorldObject
    {
        Unknown,
        Player,
        Monster,
    }

    public enum State
    {
        Die,
        Moving,
        Idle,
        Skill,
    };

public enum Layer
    {
        Monster = 8,
        Ground = 9,
        Block = 10,
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

    public enum MouseEvent
    {
        PointerDown,
        Press,
        PointerUp,
        Click,
    }

    public enum CameraMode
    {
        QuaterView, // delta (0, 6, -5) Rot (45, 0, 0)
        SoulLikeView, // delta (0, 2.6, -4) Rot (20, 0, 0)
    }
}
