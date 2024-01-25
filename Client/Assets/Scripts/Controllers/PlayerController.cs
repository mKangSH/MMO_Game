using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : BaseController
{
    int _raycastMask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);

    PlayerStat _stat;
    bool _stopSkill = false;

    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Player;

        _stat = gameObject.GetComponent<PlayerStat>();

        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;

        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
        {
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
        }
    }

    protected override void UpdateMoving()
    {
        // 몬스터가 사정거리보다 가까우면 공격
        if(_lockTarget != null)
        {
            _destPos = _lockTarget.transform.position;
            float distance = (_destPos - transform.position).magnitude;
            if(distance <= 1)
            {
                State = Define.State.Skill;
                return;
            }
        }
        
        // 이동 
        Vector3 dir = _destPos - transform.position;
        dir.y = 0;
        if (dir.magnitude < 0.1f && Input.GetMouseButton(0) == false)
        {
            State = Define.State.Idle;
        }

        else
        {
            Debug.DrawRay(transform.position + (Vector3.up * 0.5f), dir.normalized, Color.green);
            if(Physics.Raycast(transform.position + (Vector3.up * 0.5f), dir, 1.0f, LayerMask.GetMask("Block")))
            {
                if (Input.GetMouseButton(0) == false)
                {
                    State = Define.State.Idle;
                }

                return;
            }

            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;

            //transform.position = transform.position + (dir.normalized * moveDist);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        }
    }

    protected override void UpdateSkill()
    {
        if(_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }

        else
        {
            State = Define.State.Idle;
        }
    }

public class PlayerController : MonoBehaviour
{
    public float _speed = 5.0f;
    void Start()
    {
        
    }

    // deltaTime 은 모든 기기에서 게임 동작 스피드를 같게 하기 위해 곱해줌.
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * Time.deltaTime * _speed;
        }

        else if(Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * Time.deltaTime * _speed;
        }

        else if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }

        else if(Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }
    }
}
