using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private Player m_player;
    private BoxCollider2D m_attackBox;

	private void Start () {

        m_player = gameObject.GetComponent<Player>();
        m_attackBox = m_player.m_attackBox.GetComponent<BoxCollider2D>();
    }


	private void Update () {
        GetInput();
    }

    private void GetInput() {
        if (m_player != null && m_attackBox != null && m_player.m_input != null) {
            if (!m_attackBox.enabled && m_player.m_input.GetButtonDown(m_player.m_playerId, InputAction.Attack)) {
                // Other object handles setting enabled to false when it registers the collision.
                StartCoroutine(AttackTimer(m_player.m_attackBox_time));
            }else if (m_attackBox.enabled && m_player.m_input.GetButtonUp(m_player.m_playerId, InputAction.Attack)) {
                // In case we press attack but no collision was registered elsewhere.
                m_attackBox.enabled = false;
            }
        }
    }

    private IEnumerator AttackTimer(float timeToWait) {
        // Ensures that we can't press and hold the attack button and run into an object to get 1st hit.
        // This can be done with animation events later perhaps.
        m_attackBox.enabled = true;
        yield return new WaitForSeconds(timeToWait);
        m_attackBox.enabled = false;
    }
}
