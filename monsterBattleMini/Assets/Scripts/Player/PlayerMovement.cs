using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement:MonoBehaviour {

    private Player m_player;
    private BoxCollider2D m_attackBox;
    private Vector2 m_lastInputDirection;
    private Vector2 m_moveInputDirection;

    private void Start() {

        m_player = gameObject.GetComponent<Player>();
        m_attackBox = m_player.m_attackBox.GetComponent<BoxCollider2D>();
        m_lastInputDirection = Vector2.zero;
    }

    private void Update() {
        GetInput();
        Move();
        SendAnimData();
    }

    public void Move() {
        m_player.m_body.velocity = new Vector2(0, 0);

        if (m_player.m_isMoving) {

            m_lastInputDirection.x = m_moveInputDirection.x;
            m_lastInputDirection.y = m_moveInputDirection.y;

            m_player.m_body.velocity = m_moveInputDirection.normalized * m_player.m_walkSpeed;
        }
    }

    private void LateUpdate() {

        if (m_attackBox != null) {
            if (m_moveInputDirection.x < 0f) {

                Vector2 offset = m_attackBox.offset;
                offset.x = -0.5f;
                offset.y = -0.2f;
                m_attackBox.offset = offset;
            }else if (m_moveInputDirection.x > 0f) {

                Vector2 offset = m_attackBox.offset;
                offset.x = 0.5f;
                offset.y = -0.2f;
                m_attackBox.offset = offset;
            }else if (m_moveInputDirection.y < 0f) {

                Vector2 offset = m_attackBox.offset;
                offset.x = 0.0f;
                offset.y = -0.7f;
                m_attackBox.offset = offset;
            } else if (m_moveInputDirection.y > 0f) {

                Vector2 offset = m_attackBox.offset;
                offset.x = 0.0f;
                offset.y = 0.3f;
                m_attackBox.offset = offset;
            }
        }
    }

    public void GetInput() {
        m_moveInputDirection = Vector2.zero;

        if (m_player != null && m_player.m_input != null) {
            if (m_player.m_input.GetButton(m_player.m_playerId, InputAction.Up)) { m_moveInputDirection += Vector2.up; }
            if (m_player.m_input.GetButton(m_player.m_playerId, InputAction.Down)) { m_moveInputDirection += Vector2.down; }
            if (m_player.m_input.GetButton(m_player.m_playerId, InputAction.Left)) { m_moveInputDirection += Vector2.left; }
            if (m_player.m_input.GetButton(m_player.m_playerId, InputAction.Right)) { m_moveInputDirection += Vector2.right; }

            if (m_moveInputDirection != Vector2.zero) {
                m_player.m_isMoving = true;
            } else {
                m_player.m_isMoving = false;
            }
        }
    }

    private void SendAnimData() {
        m_player.m_animator.SetFloat("yDirection", m_moveInputDirection.y);
        m_player.m_animator.SetFloat("xDirection", m_moveInputDirection.x);
        m_player.m_animator.SetFloat("lastYDirection", m_lastInputDirection.y);
        m_player.m_animator.SetFloat("lastXDirection", m_lastInputDirection.x);
        m_player.m_animator.SetBool("isMoving", m_player.m_isMoving);
    }

}
