using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityEngine.Tilemaps {

    [ExecuteInEditMode]
    [Serializable]
    public class BM_PipeTile:TileBase {

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

                case 152:               // w e se
                case 56:                // w e sw
                case 25:                // nw w e
                case 28:                // ne w e
                case 29:                // nw ne w e
                case 31:                // nw n ne w e
                case 60:                // ne w e sw
                case 153:               // nw w e se
                case 159:               // nw n ne w e se
                case 184:               // w e sw se
                case 189:               // nw ne w e sw se
                case 248:               // w e sw s se
                case 24:    return 1;   // w e

                case 86:                // n ne s
                case 84:                // ne e s
                case 112:               // e sw s
                case 116:               // ne e sw s
                case 118:               // n ne e sw s
                case 120:               // w e sw s
                case 124:               // ne w e sw s
                case 127:               // nw n ne w e sw s
                case 208:               // e s se
                case 212:               // ne e s se
                case 240:               // e sw s se
                case 80:    return 2;   // e s

                case 73:                // nw w s
                case 75:                // nw n w s
                case 104:               // w sw s
                case 105:               // nw w sw s
                case 200:               // w s se
                case 201:               // nw w s se
                case 203:               // nw n w s se
                case 216:               // w e s se
                case 217:               // nw w e s se
                case 223:               // nw n ne w e s se
                case 232:               // w sw s se
                case 72:    return 3;   // w s

                case 11:                // nw n w
                case 14:                // n ne w
                case 15:                // nw n ne w
                case 30:                // n ne w e
                case 42:                // n w sw
                case 43:                // nw n w sw
                case 46:                // n ne w sw
                case 62:                // n ne w e sw
                case 106:               // n w sw s
                case 110:               // n ne w sw s
                case 254:               // n ne w e sw s se
                case 10:    return 4;   // n w

                case 19:                // nw n e
                case 22:                // n ne e
                case 23:                // nw n ne e
                case 27:                // nw n w e
                case 146:               // n e se
                case 147:               // nw n e se
                case 150:               // n ne e se
                case 155:               // nw n w e se
                case 210:               // n e s se
                case 211:               // nw n e s se
                case 251:               // nw n w e sw s se
                case 18:    return 5;   // n e

                case 20:                // ne e
                case 144:               // e se
                case 16:    return 6;   // e

                case 96:                // sw s
                case 192:               // s se
                case 64:    return 7;   // s

                case 8:                 // w
                case 40:                // w sw
                case 9:     return 8;   // nw w

                case 3:                 // nw n
                case 6:                 // n ne
                case 2:     return 9;   // n
            }
            return 0;
        }

#if UNITY_EDITOR
        [MenuItem("Assets/Create/Custom Tile/BM_PipeTile")]
        public static void CreateBM_PipeTile() {
            string path = EditorUtility.SaveFilePanelInProject("Save BitMask Pipe Tile", "New Bitmask Pipe Tile", "asset", "Save BitMask Pipe Tile", "Assets");
            if (path == "") {
                return;
            }

            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<BM_PipeTile>(), path);
        }
#endif
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(BM_PipeTile))]
    internal class BM_PipeTileEditor:Editor {
        private BM_PipeTile tile { get { return (target as BM_PipeTile); } }
        private const int k_bitSpriteCount = 10;
        private const float k_spriteWH = 16f * 4f;

        // Tile Example Sprite Icons
        private static Texture2D[] s_spriteIcons;
        private static Texture2D[] spriteIcons {
            get {
                if (s_spriteIcons == null) {
                    s_spriteIcons = new Texture2D[k_bitSpriteCount];
                    s_spriteIcons[0] = Base64ToTexture("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQAQMAAAAlPW0iAAAABlBMVEUA/wAAAABvA35DAAAADUlEQVQI12Ngf0ASAgBT9w5xQ73+hQAAAABJRU5ErkJggg==");
                    s_spriteIcons[1] = Base64ToTexture("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQAQMAAAAlPW0iAAAABlBMVEUA/wAAAABvA35DAAAAEElEQVQI12NAA///oyNUAAAfHwv1r4dpTQAAAABJRU5ErkJggg==");
                    s_spriteIcons[2] = Base64ToTexture("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQAQMAAAAlPW0iAAAABlBMVEUA/wAAAABvA35DAAAAEUlEQVQI12NAA+z/MdADZAQAsGAKqIj75hgAAAAASUVORK5CYII=");
                    s_spriteIcons[3] = Base64ToTexture("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQAQMAAAAlPW0iAAAABlBMVEUA/wAAAABvA35DAAAAEUlEQVQI12NAA/8foCN2FAQALcQPvkV6pDUAAAAASUVORK5CYII=");
                    s_spriteIcons[4] = Base64ToTexture("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQAQMAAAAlPW0iAAAABlBMVEUA/wAAAABvA35DAAAAEUlEQVQI12Ngf4CC/mMgVAAAwqcPvh0c54EAAAAASUVORK5CYII=");
                    s_spriteIcons[5] = Base64ToTexture("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQAQMAAAAlPW0iAAAABlBMVEUA/wAAAABvA35DAAAAEUlEQVQI12Ngf4CK/qMjVAAARVIKqNu1fMIAAAAASUVORK5CYII=");
                    s_spriteIcons[6] = Base64ToTexture("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQAQMAAAAlPW0iAAAABlBMVEUA/wAAAABvA35DAAAAEElEQVQI12NAA/z/0REqAACVQAZVFfP5HgAAAABJRU5ErkJggg==");
                    s_spriteIcons[7] = Base64ToTexture("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQAQMAAAAlPW0iAAAABlBMVEUA/wAAAABvA35DAAAADklEQVQI12NABuwPCCIAvgIK1WKud4YAAAAASUVORK5CYII=");
                    s_spriteIcons[8] = Base64ToTexture("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQAQMAAAAlPW0iAAAABlBMVEUA/wAAAABvA35DAAAAEElEQVQI12NAA/8/oCNUAAAW3Aubql5w5QAAAABJRU5ErkJggg==");
                    s_spriteIcons[9] = Base64ToTexture("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQAQMAAAAlPW0iAAAABlBMVEUA/wAAAABvA35DAAAADklEQVQI12Ngf0AYIQEAQAEK1SViy9MAAAAASUVORK5CYII=");
                }
                return s_spriteIcons;
            }
        }

        public void OnEnable() {
            // Reset size of sprite array if it has somehow changed (Defensive coding for future users)
            if (tile.m_BitSprites == null || tile.m_BitSprites.Length != k_bitSpriteCount) {
                tile.m_BitSprites = new Sprite[k_bitSpriteCount];
                EditorUtility.SetDirty(tile);
            }
        }

        public override void OnInspectorGUI() {
            createEditorLayout();
        }

        private void createEditorLayout() {

            EditorGUILayout.LabelField("Bitmask Pipe Tile");
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
