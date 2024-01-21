using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    int _raycastMask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);

    Texture2D _attackCursorIcon;
    Texture2D _handCursorIcon;

    Vector2 _attackCursorOffset;
    Vector2 _handCursorOffset;

    enum CursorType
    {
        None,
        Attack,
        Hand,
    }

    CursorType _cursorType = CursorType.None;

    void Start()
    {
        _attackCursorIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Attack");
        _handCursorIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Hand");

        _attackCursorOffset = new Vector2(_attackCursorIcon.width / 5, 0);
        _handCursorOffset = new Vector2(_handCursorIcon.width / 4, 0);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, _raycastMask))
        {
            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
            {
                if (_cursorType != CursorType.Attack)
                {
                    Cursor.SetCursor(_attackCursorIcon, _attackCursorOffset, CursorMode.Auto);
                    _cursorType = CursorType.Attack;
                }
            }

            else if (hit.collider.gameObject.layer == (int)Define.Layer.Ground)
            {
                if (_cursorType != CursorType.Hand)
                {
                    Cursor.SetCursor(_handCursorIcon, _handCursorOffset, CursorMode.Auto);
                    _cursorType = CursorType.Hand;
                }
            }
        }
    }
}
