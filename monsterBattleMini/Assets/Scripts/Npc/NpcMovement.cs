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

    private float m_idleDirectionChangeTimer;
    private float m_idleDirectionWaitTime;

	void Start () {

        m_npc = gameObject.GetComponent<Npc>();
        m_lastDirection = m_moveDirection = Vector2.zero;
        m_direction = direction.down;

        m_directionVectors = new Vector2[4];

        m_directionVectors[(int)direction.up] = Vector2.up;
        m_directionVectors[(int)direction.down] = Vector2.down;
        m_directionVectors[(int)direction.left] = Vector2.left;
        m_directionVectors[(int)direction.right] = Vector2.right;

        m_idleDirectionChangeTimer = 0f;
        m_idleDirectionWaitTime = Random.Range(1f, 5f);
    }

	void Update () {
        SetMoveDirection();
        SetIdleFacingDirection();
        Move();
        SendAnimData();
	}


    private void SetMoveDirection() {
        m_moveDirection = Vector2.zero;

    }

    private void SetIdleFacingDirection() {
        if (!m_npc.m_isMoving && m_idleDirectionChangeTimer == 0f) {
            m_direction = (direction)Mathf.RoundToInt(Random.Range(0f, 3f));
        }

        m_idleDirectionChangeTimer += Time.deltaTime;

        if(m_idleDirectionChangeTimer >= m_idleDirectionWaitTime) {
            m_idleDirectionChangeTimer = 0f;
            m_idleDirectionWaitTime = Random.Range(1f, 5f);
        }
    }

    private void Move() {
        m_npc.m_body.velocity = new Vector2(0f, 0f);

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
