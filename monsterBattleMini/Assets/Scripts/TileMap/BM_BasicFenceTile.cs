using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityEngine.Tilemaps {

    [Serializable]
    public class BM_BasicFenceTile:TileBase {

        [SerializeField]
        public Sprite[] m_BitSprites;
        [SerializeField]
        public Tile.ColliderType m_TileColliderType = Tile.ColliderType.None;

        public override void GetTileData(Vector3Int location, ITilemap tilemap, ref TileData tileData) {

            if (m_BitSprites == null || m_BitSprites.Length <= 0) return;

            UpdateTile(location, tilemap, ref tileData);
        }

        public override void RefreshTile(Vector3Int location, ITilemap tilemap) {
            checkNeighboringTiles(location, tilemap);
        }

        // Looks for tiles around this tile and calls RefreshTile on any found.
        public void checkNeighboringTiles(Vector3Int location, ITilemap tilemap) {
            for (int yd = -1; yd <= 1; yd++) {
                for (int xd = -1; xd <= 1; xd++) {
                    Vector3Int position = new Vector3Int(location.x + xd, location.y + yd, location.z);
                    if (NeighboringTileAtPos(tilemap, position))
                        tilemap.RefreshTile(position);
                }
            }
        }

        private bool NeighboringTileAtPos(ITilemap tileMap, Vector3Int position) {
            TileBase tile = tileMap.GetTile(position);
            return (tile != null && tile == this);
        }

        private void UpdateTile(Vector3Int location, ITilemap tilemap, ref TileData tileData) {
            tileData.transform = Matrix4x4.identity;
            tileData.color = Color.white;
            tileData.colliderType = m_TileColliderType;

            // 1 is x: right y: up.
            int mask = NeighboringTileAtPos(tilemap, location + new Vector3Int(-1, 1, 0)) ? 1 : 0;  // nw
            mask += NeighboringTileAtPos(tilemap, location + new Vector3Int(0, 1, 0)) ? 2 : 0;      // n
            mask += NeighboringTileAtPos(tilemap, location + new Vector3Int(1, 1, 0)) ? 4 : 0;      // ne
            mask += NeighboringTileAtPos(tilemap, location + new Vector3Int(-1, 0, 0)) ? 8 : 0;     // w
            mask += NeighboringTileAtPos(tilemap, location + new Vector3Int(1, 0, 0)) ? 16 : 0;     // e
            mask += NeighboringTileAtPos(tilemap, location + new Vector3Int(-1, -1, 0)) ? 32 : 0;   // sw
            mask += NeighboringTileAtPos(tilemap, location + new Vector3Int(0, -1, 0)) ? 64 : 0;    // s
            mask += NeighboringTileAtPos(tilemap, location + new Vector3Int(1, -1, 0)) ? 128 : 0;   // se

            int bitMaskValue = NeighborsToTileIndex(mask);

            if (bitMaskValue >= 0 && bitMaskValue < m_BitSprites.Length && NeighboringTileAtPos(tilemap, location)) {

                tileData.sprite = m_BitSprites[bitMaskValue];
            }
        }

        private int NeighborsToTileIndex(int mask) {
            // Cases grouped by sprite selection.

            switch (mask) {

                case 11: return 1;      // n w

                case 22: return 2;      // n e

                case 104: return 3;     // w s

                case 107: return 4;     // nw n w sw s

                case 208: return 5;     // e s

                case 2:                 // n
                case 64:                // s
                case 66:                // n s
                case 214: return 6;     // n ne e s se

                case 255: return 8;     // all
            }
            return 0;
        }

#if UNITY_EDITOR
        [MenuItem("Assets/Create/Custom Tile/BM_BasicFenceTile")]
        public static void CreateBM_BasicFenceTile() {
            string path = EditorUtility.SaveFilePanelInProject("Save BitMask Basic Fence Tile", "New Bitmask Basic Fence Tile", "asset", "Save BitMask Basic Fence Tile", "Assets");
            if (path == "") {
                return;
            }

            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<BM_BasicFenceTile>(), path);
        }
#endif
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(BM_BasicFenceTile))]
    internal class BM_BasicFenceTileEditor:Editor {
        private BM_BasicFenceTile tile { get { return (target as BM_BasicFenceTile); } }
        private const int k_bitSpriteCount = 9;
        private const float k_spriteWH = 16f * 4f;

        // Tile Example Sprite Icons
        private static Texture2D[] s_spriteIcons;
        private static Texture2D[] spriteIcons {
            get {
                if (s_spriteIcons == null) {
                    s_spriteIcons = new Texture2D[k_bitSpriteCount];
                    s_spriteIcons[0] = Base64ToTexture("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQAQMAAAAlPW0iAAAABlBMVEUB/wAAAACkX63mAAAAEElEQVQI12OAA/kf+BAMAAABkgrnNcHeUAAAAABJRU5ErkJggg==");
                    s_spriteIcons[1] = Base64ToTexture("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQAQMAAAAlPW0iAAAABlBMVEUA/wAAAABvA35DAAAAEUlEQVQI12Ngf4CC/mMgVAAAwqcPvh0c54EAAAAASUVORK5CYII=");
                    s_spriteIcons[2] = Base64ToTexture("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQAQMAAAAlPW0iAAAABlBMVEUA/wAAAABvA35DAAAAEUlEQVQI12Ngf4CK/qMjVAAARVIKqNu1fMIAAAAASUVORK5CYII=");
                    s_spriteIcons[3] = Base64ToTexture("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQAQMAAAAlPW0iAAAABlBMVEUA/wAAAABvA35DAAAAEUlEQVQI12NAA/8foCN2FAQALcQPvkV6pDUAAAAASUVORK5CYII=");
                    s_spriteIcons[4] = Base64ToTexture("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQAQMAAAAlPW0iAAAABlBMVEUAAAAB/wA3gS6KAAAADUlEQVQI12NgYCcJAQAKeABxy0hRBgAAAABJRU5ErkJggg==");
                    s_spriteIcons[5] = Base64ToTexture("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQAQMAAAAlPW0iAAAABlBMVEUA/wAAAABvA35DAAAAEUlEQVQI12NAA+z/MdADZAQAsGAKqIj75hgAAAAASUVORK5CYII=");
                    s_spriteIcons[6] = Base64ToTexture("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQAQMAAAAlPW0iAAAABlBMVEUAAAAB/wA3gS6KAAAADUlEQVQI12N4wEASAgBXPw4BoMIRRgAAAABJRU5ErkJggg==");
                    s_spriteIcons[7] = Base64ToTexture("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQAQMAAAAlPW0iAAAABlBMVEUA/wAAAABvA35DAAAAEElEQVQI12NAA///oyNUAAAfHwv1r4dpTQAAAABJRU5ErkJggg==");
                    s_spriteIcons[8] = Base64ToTexture("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQAQMAAAAlPW0iAAAAA1BMVEUB/wA1nKqfAAAAC0lEQVQI12MgEQAAADAAAWV61nwAAAAASUVORK5CYII=");
                }
                return s_spriteIcons;
            }
        }

        public void OnEnable() {
            // Reset size of sprite array if it has somehow changed (Defensive coding for future users)
            if (tile.m_BitSprites == null || tile.m_BitSprites.Length != k_bitSpriteCount) {
                tile.m_BitSprites = new Sprite[k_bitSpriteCount];
            }
        }

        public override void OnInspectorGUI() {
            createEditorLayout();
        }

        private void createEditorLayout() {

            EditorGUILayout.LabelField("Bitmask Basic Fence Tile");
            EditorGUILayout.LabelField("Place sprites to match the example images.");
            EditorGUILayout.LabelField("Sprites required " + k_bitSpriteCount);

            EditorGUILayout.Space();

            GUI.changed = false;

            // TODO: Make editor option to have individual ColliderType values for each sprite. If selected add a ColliderType popup for every sprite
            tile.m_TileColliderType = (Tile.ColliderType)EditorGUILayout.EnumPopup("Collider Type", tile.m_TileColliderType);

            EditorGUILayout.Space();

            // Draw actual tile sprite fields. These are used to assign the correct sprite to the tile when placing
            for (int i = 0; i < k_bitSpriteCount; i++) {
                Rect r = (Rect)EditorGUILayout.BeginVertical();
                tile.m_BitSprites[i] = (Sprite)EditorGUILayout.ObjectField("Sprite " + (i), tile.m_BitSprites[i], typeof(Sprite), false);
                spriteIcons[i].filterMode = FilterMode.Point;
                EditorGUI.DrawPreviewTexture(new Rect((EditorGUIUtility.currentViewWidth) - (k_spriteWH * 2.5f), r.y, k_spriteWH, k_spriteWH), spriteIcons[i]);
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space();
            }

            if (GUI.changed) {
                EditorUtility.SetDirty(tile);
            }
        }

        private static Texture2D Base64ToTexture(string base64) {
            Texture2D t = new Texture2D(1, 1);
            t.hideFlags = HideFlags.HideAndDontSave;
            t.LoadImage(System.Convert.FromBase64String(base64));
            return t;
        }
    }
#endif
}
