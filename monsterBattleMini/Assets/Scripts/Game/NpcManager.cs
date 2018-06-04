using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour {

    public GameObject[] m_mobs;

	private void Awake () {
        m_mobs = GameObject.FindGameObjectsWithTag("Mob");
        Array.Sort(m_mobs, CompareObjNames);

        for (int i = 0; i < m_mobs.Length; i++) {
            Mob mob = m_mobs[i].GetComponent<Mob>();
            mob.m_npcId = i;
        }
    }

    private int CompareObjNames(GameObject a, GameObject b) {
        return a.name.CompareTo(b.name);
    }
}
