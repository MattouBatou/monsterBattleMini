using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : Npc {

    [HideInInspector]
    public CollectableObject m_collectable;
    [HideInInspector]
    public MobMovement m_movement;

    protected override void Start() {
        base.Start();
        m_collectable = gameObject.AddComponent<CollectableObject>();
        m_movement = gameObject.AddComponent<MobMovement>();

        AddBoxCollider2D(ref m_hitbox, gameObject.name + " _Hit_Box", "MobHitBox", Vector2.one, new Vector2(0f, -0.44f), true, true);
        // Add Mob specific stuff here.
    }

    private void OnTriggerEnter2D(Collider2D other) {

        switch (other.tag) {

            case "PlayerHitBox":

                if (m_followTarget == null && other.transform.parent.gameObject) {
                    m_followTarget = other.transform.parent.gameObject.GetComponent<Player>();
                }

            break;
        }
    }
}
