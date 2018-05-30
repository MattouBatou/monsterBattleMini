using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class MobSprites:MonoBehaviour {

    [SerializeField]
    [HideInInspector]
    public Sprite[] m_sprites;

    [Header("Idle Sprites")]
    [SerializeField]
    public Sprite m_idleUp;
    [SerializeField]
    public Sprite m_idleDown;
    [SerializeField]
    public Sprite m_idleLeft;
    [SerializeField]
    public Sprite m_idleRight;

    [Header("Walk Sprites")]
    [SerializeField]
    public Sprite[] m_walkUp;
    [SerializeField]
    public Sprite[] m_walkDown;
    [SerializeField]
    public Sprite[] m_walkLeft;
    [SerializeField]
    public Sprite[] m_walkRight;

#if UNITY_EDITOR
    [CustomEditor(typeof(MobSprites))]
    internal class MobSpritesEditor:Editor {

        private MobSprites mobSprites { get { return (target as MobSprites); } }

        private void OnEnable() {
            if(mobSprites.m_sprites != null) {
                EditorUtility.SetDirty(mobSprites);
            }
        }
    }
#endif
}
