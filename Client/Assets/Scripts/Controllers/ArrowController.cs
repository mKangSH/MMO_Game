using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class ArrowController : CreatureController
{
    protected override void Init()
    {
        switch (_lastDir)
        {
            case MoveDir.Up:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case MoveDir.Down:
                transform.rotation = Quaternion.Euler(0, 0, -180);
                break;
            case MoveDir.Left:
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case MoveDir.Right:
                transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
        }
        base.Init();
    }

    protected override void UpdateAnimation()
    {

    }

    protected override void UpdateIdle()
    {
        base.UpdateIdle();

        if (Managers.Map.CanGo(_destPos))
        {
            GameObject go = Managers.Object.Find(_destPos);
            if (go == null)
            {
                CellPos = _destPos;
                State = CreatureState.Moving;
            }
            else
            {
                CreatureController cc = go.GetComponent<CreatureController>();
                if(cc != null)
                {
                    cc.OnDamaged();
                }
                Managers.Resource.Destroy(gameObject);
            }
        }

        else
        {
            Managers.Resource.Destroy(gameObject);
        }
    }
}
