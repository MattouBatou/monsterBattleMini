using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteInEditMode]
public class TileMapStats:MonoBehaviour {

    public Vector3Int tilemapSize;
    private Tilemap tilemap;

    public void Start() {

        tilemap = GetComponent<Tilemap>();
    }

    public void Update() {

        tilemapSize = tilemap.editorPreviewSize;
    }
}
