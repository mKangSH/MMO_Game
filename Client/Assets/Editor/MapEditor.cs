using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MapEditor
{
#if UNITY_EDITOR
    [MenuItem("Tools/GenerateMap %#&p")]
    private static void GenerateMap()
    {
        GameObject[] gameObjects = Resources.LoadAll<GameObject>("Prefabs/Map");
        foreach (GameObject gameObject in gameObjects)
        {
            Tilemap tm = Util.FindChild<Tilemap>(gameObject, "Tilemap_Collision", true);

            using (var writer = File.CreateText($"Assets/Resources/Map/{gameObject.name}.txt"))
            {
                writer.WriteLine(tm.cellBounds.xMin);
                writer.WriteLine(tm.cellBounds.xMax);
                writer.WriteLine(tm.cellBounds.yMin);
                writer.WriteLine(tm.cellBounds.yMax);

                for (int y = tm.cellBounds.yMax; y >= tm.cellBounds.yMin; y--)
                {
                    for (int x = tm.cellBounds.xMin; x <= tm.cellBounds.xMax; x++)
                    {
                        TileBase tile = tm.GetTile(new Vector3Int(x, y, 0));
                        if (tile != null)
                        {
                            writer.Write("1");
                        }
                        else
                        {
                            writer.Write("0");
                        }
                    }

                    writer.WriteLine();
                }
            }
        }
    }
#endif
}
