using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other) {

        switch (other.tag) {
            case "PlayerHitBox":

                if (tag == "Collectable") {

                    GameObject.Destroy(gameObject);
                }

            break;
        }
    }
}
