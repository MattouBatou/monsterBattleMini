using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileMapStats))]
public class TileMapStatsEditor : Editor {

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        DrawDefaultInspector();

        TileMapStats tilemapStats = (TileMapStats)target;
        if(GUILayout.Button("Show Tilemap Stats")) {
            tilemapStats.showStats();
        }
    }
}
