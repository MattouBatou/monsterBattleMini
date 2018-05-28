using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakableObject : MonoBehaviour {

    public int m_health = 2;

    private void OnTriggerEnter2D(Collider2D collision) {
        switch (collision.tag) {
            case "PlayerAttackBox":
                reduceHealth(-1);
                break;
        }
    }

    private void breakObject() {
        GameObject.Destroy(gameObject);
    }

    private void reduceHealth(int damageVal) {
        m_health += damageVal;
        if(m_health <= 0) {
            breakObject();
        }
    }
}
