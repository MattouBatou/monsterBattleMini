using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMovement:MonoBehaviour {

    private Npc m_npc;
    private Vector2 m_lastDirection;
    private Vector2 m_moveDirection;

    [HideInInspector]
    public enum direction {
        up,
        down,
        left,
        right
    }

    [HideInInspector]
    public Vector2[] m_directionVectors;

    [HideInInspector]
    public direction m_direction;

	void Start () {

        m_npc = gameObject.GetComponent<Npc>();
        m_lastDirection = m_moveDirection = Vector2.zero;
        m_direction = direction.down;

        m_directionVectors[(int)direction.up] = Vector2.up;
        m_directionVectors[(int)direction.down] = Vector2.down;
        m_directionVectors[(int)direction.left] = Vector2.left;
        m_directionVectors[(int)direction.right] = Vector2.right;
    }

	void Update () {
        SetMoveDirection();
        Move();
        SendAnimData();
	}


    private void SetMoveDirection() {
        m_moveDirection = Vector2.zero;

        if (m_npc != null) {
            if(m_direction == direction.down) {

            }
        }
    }

    private void Move() {
        m_npc.m_body.velocity = new Vector2(0, 0);

        if (m_npc.m_isMoving) {
            m_lastDirection = m_moveDirection;
        }
    }

    private void SendAnimData() {
        m_npc.m_animator.SetFloat("yDirection", m_moveDirection.y);
        m_npc.m_animator.SetFloat("xDirection", m_moveDirection.x);
        m_npc.m_animator.SetFloat("lastYDirection", m_lastDirection.y);
        m_npc.m_animator.SetFloat("lastXDirection", m_lastDirection.x);
        m_npc.m_animator.SetFloat("xIdleDirection", m_directionVectors[(int)m_direction].x);
        m_npc.m_animator.SetFloat("yIdleDirection", m_directionVectors[(int)m_direction].y);
        m_npc.m_animator.SetBool("isMoving", m_npc.m_isMoving);
    }
}
