using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MonsterController : CreatureController
{
    protected override void Init()
    {
        // base.Init 에서 애니메이터를 연결해주고 
        // State, Dir 변경 시에 애니메이션 실행이 되니깐 순서 변경은 하지 말아야 한다.
        base.Init();

        State = CreatureState.Idle;
        Dir = MoveDir.None;
    }

    protected override void UpdateController()
    {
        // GetDirectionInput();
        base.UpdateController();
    }

    void GetDirectionInput()
    {
        if (State == CreatureState.Moving && Input.anyKeyDown == true)
        {
            return;
        }

        if (Input.GetKey(KeyCode.W))
        {
            //transform.position += Vector3.up * Time.deltaTime * _speed;
            Dir = MoveDir.Up;
        }

        else if (Input.GetKey(KeyCode.S))
        {
            //transform.position += Vector3.down * Time.deltaTime * _speed;
            Dir = MoveDir.Down;
        }

        else if (Input.GetKey(KeyCode.A))
        {
            //transform.position += Vector3.left * Time.deltaTime * _speed;
            Dir = MoveDir.Left;
        }

        else if (Input.GetKey(KeyCode.D))
        {
            //transform.position += Vector3.right * Time.deltaTime * _speed;
            Dir = MoveDir.Right;
        }

        else
        {
            Dir = MoveDir.None;
        }
    }

    public override void OnDamaged()
    {
        base.OnDamaged();

        GameObject effect = Managers.Resource.Instantiate("Effect/DieEffect");
        effect.transform.position = transform.position;
        effect.GetComponent<Animator>().Play("START");
        GameObject.Destroy(effect, 0.5f);

        Managers.Object.Remove(gameObject);
        Managers.Resource.Destroy(gameObject);
    }
}
