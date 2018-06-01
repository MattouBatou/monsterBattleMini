using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other) {
        switch (other.tag) {
            case "Player":
                if (tag == "Collectable") {
                    GameObject.Destroy(gameObject);
                } else if(tag == "Mob"){
                    transform.position = other.transform.position;
                }

                Debug.Log(tag + " collided with player");
                break;
        }
    }
}
