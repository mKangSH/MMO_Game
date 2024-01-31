using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerController : CreatureController
{
    KeyCode _prevKey = KeyCode.None;

    protected override void Init()
    {
        base.Init();
    }

    protected override void UpdateController()
    {
        GetDirectionInput();
        base.UpdateController();
    }

    private void LateUpdate()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    void GetDirectionInput()
    {
        if(State == CreatureState.Moving && Input.GetKey(_prevKey))
        {
            return;
        }

        if (Input.GetKey(KeyCode.W))
        {
            //transform.position += Vector3.up * Time.deltaTime * _speed;
            _prevKey = KeyCode.W;
            Dir = MoveDir.Up;
        }

        else if (Input.GetKey(KeyCode.S))
        {
            //transform.position += Vector3.down * Time.deltaTime * _speed;
            _prevKey = KeyCode.S;
            Dir = MoveDir.Down;
        }

        else if (Input.GetKey(KeyCode.A))
        {
            //transform.position += Vector3.left * Time.deltaTime * _speed;
            _prevKey = KeyCode.A;
            Dir = MoveDir.Left;
        }

        else if (Input.GetKey(KeyCode.D))
        {
            //transform.position += Vector3.right * Time.deltaTime * _speed;
            _prevKey = KeyCode.D;
            Dir = MoveDir.Right;
        }

        else
        {
            Dir = MoveDir.None;
        }
    }
}
