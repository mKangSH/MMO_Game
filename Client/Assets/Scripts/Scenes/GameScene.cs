using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;
        // Managers.UI.ShowSceneUI<UI_Inven>();
        gameObject.GetOrAddComponent<CursorController>();

        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        
        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "UnityChan");
        Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);

        // Managers.Game.Spawn(Define.WorldObject.Monster, "Knight");
        GameObject go = new GameObject { name = "SpawningPool" };
        go.transform.position = Vector3.back * 25;

        SpawningPool pool = go.GetOrAddComponent<SpawningPool>();
        pool.SetSpawnPos(go.transform.position);
        pool.SetKeepMonsterCount(5);

    }

    public override void Clear()
    {
        
    }
}
