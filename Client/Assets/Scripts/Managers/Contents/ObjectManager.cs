using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
    // Dictionary<int, GameObject> _objects = new Dictionary<int, GameObject>();
    List<GameObject> _objects = new List<GameObject>();

    public void Add(GameObject go)
    {
        _objects.Add(go);
    }

    public void Remove(GameObject go)
    {
        _objects.Remove(go);
    }

    // O(N)으로 매우매우 느릴 가능성이 높다.
    public GameObject Find(Vector3Int cellPos)
    {
        foreach(GameObject obj in _objects)
        {
            CreatureController cc = obj.GetComponent<CreatureController>();
            if(cc == null)
            {
                continue;
            }

            if(cc.CellPos == cellPos)
            {
                return obj;
            }
        }

        return null;
    }

    public void Clear()
    {
        _objects.Clear();
    }
}
