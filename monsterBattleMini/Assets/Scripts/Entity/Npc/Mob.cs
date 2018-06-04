using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : Npc {

    [HideInInspector]
    new public Player M_followTarget {
        get { return (Player)base.M_followTarget; }
        set { base.M_followTarget = value; }
    }
    [HideInInspector]
    public CollectableObject m_collectable;
    [HideInInspector]
    public MobMovement m_movement;
    [HideInInspector]
    public int ownerId = -1;

    new public void Start() {
        base.Start();
        m_collectable = gameObject.AddComponent<CollectableObject>();
        m_movement = gameObject.AddComponent<MobMovement>();

        AddBoxCollider2D(ref m_hitbox, gameObject.name + " _Hit_Box", "MobHitBox", Vector2.one, new Vector2(0f, -0.44f), true, true);
        // Add Mob specific stuff here.
    }

    private void OnTriggerEnter2D(Collider2D other) {

        switch (other.tag) {

            case "PlayerHitBox":

                if (ownerId == -1 && M_followTarget == null && other.transform.parent.gameObject) {

                    M_followTarget = other.transform.parent.gameObject.GetComponent<Player>();
                    if (M_followTarget.m_mobs != null && M_followTarget.m_mobs.Count < M_followTarget.m_maxMobs) {
                        ownerId = M_followTarget.m_playerId;
                        M_followTarget.m_mobs.Add(this);
                    } else {
                        M_followTarget = null;
                    }
                }

            break;
        }
    }
}
