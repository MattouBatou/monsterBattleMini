// Saving as reference for how to update the editor with custom features on update.
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

//[CustomEditor(typeof(TileMapStats))]
//public class TileMapStatsEditor:Editor {

//    private static Vector3Int tilemapSize;
//    private static int uniqueTilesUsed;

//    private void Awake() {
//        EditorApplication.update += UpdateEditor;
//    }

//    private void OnEnable() {
//        TileMapStats tilemapStats = (TileMapStats)target;
//        tilemapSize = tilemapStats.tilemapSize;
//        uniqueTilesUsed = tilemapStats.uniqueTilesUsed;
//    }

//    private void OnDisable() {
//        EditorApplication.update -= UpdateEditor;
//    }

//    private void UpdateEditor() {

//        EditorGUILayout.Vector3Field("Tilemap Size", tilemapSize);
//        EditorGUILayout.IntField(uniqueTilesUsed);
//    }
//}
