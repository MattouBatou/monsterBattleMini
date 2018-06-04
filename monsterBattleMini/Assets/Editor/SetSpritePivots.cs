using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class MenuItems:MonoBehaviour {

    [MenuItem("Sprites/Paste default Slices-Pivots")]
    static void PasteDefaultSlicesPivotsCharacter() {

        Object[] textures = GetSelectedTextures();
        Selection.objects = new Object[0];

        foreach (Texture2D texture in textures) {
            string path = AssetDatabase.GetAssetPath(texture);
            TextureImporter ti = AssetImporter.GetAtPath(path) as TextureImporter;
            ti.isReadable = true;

            List<SpriteMetaData> newData = new List<SpriteMetaData>();
            for (int i = 0; i < ti.spritesheet.Length; i++) {
                SpriteMetaData d = ti.spritesheet[i];
                List<SpriteMetaData> defaultData = new List<SpriteMetaData>();
                d.pivot = ti.spritesheet[0].pivot;

                newData.Add(d);
            }

            ti.spritesheet = newData.ToArray();
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
        }
    }

    static Object[] GetSelectedTextures() {
        return Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
    }
}
