using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectableObject : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        switch (collision.tag) {
            case "Player":
                Debug.Log("Collided with player");
                GameObject.Destroy(gameObject);
                break;
        }
    }
}
