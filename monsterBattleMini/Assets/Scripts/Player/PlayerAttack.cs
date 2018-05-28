using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private Player m_player;
    private BoxCollider2D m_attackBox;
    private bool attacking = false;

	private void Start () {

        m_player = gameObject.GetComponent<Player>();
        m_attackBox = m_player.m_attackBox.GetComponent<BoxCollider2D>();
    }


	private void FixedUpdate () {
        GetInput();
    }

    private void GetInput() {
        if (m_player != null && m_attackBox != null && m_player.m_input != null) {
            if (!attacking) {
                if (m_player.m_input.GetButtonDown(m_player.m_playerId, InputAction.Attack)) {
                    StartCoroutine(DisableCollider());
                }
            } else {
                if (m_player.m_input.GetButtonUp(m_player.m_playerId, InputAction.Attack)) {
                    m_attackBox.enabled = false;
                    attacking = false;
                    Debug.Log("Button up");
                }
            }
        }
    }

    private IEnumerator DisableCollider() {
        m_attackBox.enabled = true;
        attacking = true;
        yield return new WaitForSeconds(0.1f);
        m_attackBox.enabled = false;
    }
}
