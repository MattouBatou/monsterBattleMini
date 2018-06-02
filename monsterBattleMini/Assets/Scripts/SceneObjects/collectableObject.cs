using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour {

    [HideInInspector]
    public NpcMovement npc;

    private void Start() {
        npc = gameObject.GetComponent<NpcMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other) {

        switch (other.tag) {
            case "Player":
                if (tag == "Collectable") {
                    GameObject.Destroy(gameObject);
                } else if(tag == "Mob"){
                    if(npc.m_followTarget == null) {
                        npc.m_followTarget = other.gameObject.GetComponent<Rigidbody2D>();
                    }
                }

                break;
        }
    }
}
