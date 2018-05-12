using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileMapStats))]
public class TileMapStatsEditor:Editor {

    private static Vector3Int tilemapSize;

    private void OnEnable() {
        EditorApplication.update += Update;
        TileMapStats tilemapStats = (TileMapStats)target;
        tilemapSize = tilemapStats.tilemapSize;
    }

    private void OnDisable() {
        EditorApplication.update -= Update;
    }

    private void Update() {

        EditorGUILayout.Vector3Field("Tilemap Size", tilemapSize);
    }
}
