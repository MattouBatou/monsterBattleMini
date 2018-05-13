using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteInEditMode]
public class TileMapStats:MonoBehaviour {

    private Tilemap tilemap;

    public BoundsInt tilemapBounds;
    public int uniqueTilesUsed;
    public int activeTileCount;
    public bool checkActiveTileCount = false;

    public void Start() {

        tilemap = GetComponent<Tilemap>();
    }

    public void Update() {

        tilemapBounds = tilemap.cellBounds;
        uniqueTilesUsed = tilemap.GetUsedTilesCount();

        if (checkActiveTileCount) {
            TileBase[] tileArray = tilemap.GetTilesBlock(tilemapBounds);
            activeTileCount = 0;
            for (int index = 0; index < tileArray.Length; index++) {
                if (tileArray[index] != null) {
                    activeTileCount++;
                }
            }
        }
    }
}
