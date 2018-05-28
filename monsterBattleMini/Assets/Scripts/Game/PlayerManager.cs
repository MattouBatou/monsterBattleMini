using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public GameObject[] m_players;

    private void Awake() {
        m_players = GameObject.FindGameObjectsWithTag("Player");
        Array.Sort(m_players, CompareObNames);

        for (int i = 0; i < m_players.Length; i++) {
            Player player = m_players[i].GetComponent<Player>();
            player.m_playerId = i;
        }
    }

    private int CompareObNames(GameObject x, GameObject y) {
        return x.name.CompareTo(y.name);
    }
}
