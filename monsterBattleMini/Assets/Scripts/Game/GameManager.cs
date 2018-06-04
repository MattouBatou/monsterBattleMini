using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [HideInInspector]
    public PlayerManager m_playerManager;
    [HideInInspector]
    public NpcManager m_npcManager;

    // TODO: Add spawn points.
    // TODO: Add number of players and create them at startup at different random spawn points.

    private void Awake() {
        m_playerManager = gameObject.AddComponent<PlayerManager>();
        m_npcManager = gameObject.AddComponent<NpcManager>();
    }

}
