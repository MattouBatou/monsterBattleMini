using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapStats:MonoBehaviour {
    public void showStats() {
        Tilemap tilemap = GetComponent<Tilemap>();

        Vector3Int tilemapSize = tilemap.size;
        Vector3Int tilemapEditorPreviewSize = tilemap.editorPreviewSize;
        BoundsInt tilemapBounds = tilemap.cellBounds;

        Debug.Log("TilemapSize: " + tilemapSize + " | TilemapEditorPreviewSize: " + tilemapEditorPreviewSize);
        Debug.Log("TilemapBounds: " + tilemapBounds);
    }
}
