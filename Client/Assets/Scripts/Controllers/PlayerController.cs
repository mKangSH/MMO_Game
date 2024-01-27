using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerController : MonoBehaviour
{
    public float _speed = 5.0f;

    Vector3Int _cellPos = Vector3Int.zero;
    MoveDir _dir = MoveDir.None;
    bool _isMoving = false;

    void Start()
    {
        
    }

    // deltaTime 은 모든 기기에서 게임 동작 스피드를 같게 하기 위해 곱해줌.
    void Update()
    {
        GetDirectionInput();
        if (_isMoving == false)
        {
            switch(_dir)
            {
                case MoveDir.None:
                break;
                case MoveDir.Left:
                break;
                case MoveDir.Right:
                break;
                case MoveDir.Up:
                break;
                case MoveDir.Down:
                break;
            }
        }
    }

    void GetDirectionInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //transform.position += Vector3.up * Time.deltaTime * _speed;
            _dir = MoveDir.Up;
        }

        else if (Input.GetKey(KeyCode.S))
        {
            //transform.position += Vector3.down * Time.deltaTime * _speed;
            _dir = MoveDir.Down;
        }

        else if (Input.GetKey(KeyCode.A))
        {
            //transform.position += Vector3.left * Time.deltaTime * _speed;
            _dir = MoveDir.Left;
        }

        else if (Input.GetKey(KeyCode.D))
        {
            //transform.position += Vector3.right * Time.deltaTime * _speed;
            _dir = MoveDir.Right;
        }

        else
        {
            _dir = MoveDir.None;
        }
    }
}
