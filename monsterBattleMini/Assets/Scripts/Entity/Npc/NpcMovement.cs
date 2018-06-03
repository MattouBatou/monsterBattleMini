using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NpcMovement:MonoBehaviour {

    [HideInInspector]
    public Npc m_npc;
    public Rigidbody2D m_followTarget;
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

	protected void Start () {

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

    protected void FixedUpdate() {
        Move();
        GetMove();
        SendAnimData();
    }

    protected void Update () {
        SetMoveDirection();
        SetIdleFacingDirection();
	}


    protected void SetMoveDirection() {
        m_moveDirection = Vector2.zero;

    }

    protected void SetIdleFacingDirection() {
        if (!m_npc.m_isMoving && m_idleDirectionChangeTimer == 0f) {
            m_direction = (direction)Mathf.RoundToInt(Random.Range(0f, 3f));
        }

        m_idleDirectionChangeTimer += Time.deltaTime;

        if(m_idleDirectionChangeTimer >= m_idleDirectionWaitTime) {
            m_idleDirectionChangeTimer = 0f;
            m_idleDirectionWaitTime = Random.Range(1f, 5f);
        }
    }

    protected void GetMove() {
        m_moveDirection = Vector2.zero;

        if (m_npc.m_body) {
            if (m_npc.m_body.velocity.x < 0f) {
                m_direction = direction.left;
                m_moveDirection += m_directionVectors[(int)m_direction];
            }
            if (m_npc.m_body.velocity.x > 0f) {
                m_direction = direction.right;
                m_moveDirection += m_directionVectors[(int)m_direction];
            }
            if (m_npc.m_body.velocity.y < 0f) {
                m_direction = direction.down;
                m_moveDirection += m_directionVectors[(int)m_direction];
            }
            if (m_npc.m_body.velocity.y > 0f) {
                m_direction = direction.up;
                m_moveDirection += m_directionVectors[(int)m_direction];
            }
        }

        if (m_moveDirection != Vector2.zero) {
            m_npc.m_isMoving = true;
        } else {
            m_npc.m_isMoving = false;
        }
    }

    protected virtual void Move() {
        m_npc.m_body.velocity = Vector2.zero;

        if (m_npc.m_followTarget != null) {
            m_npc.m_body.velocity = m_npc.m_followTarget.velocity;
        }

        if (m_npc.m_isMoving) {
            m_lastDirection = m_moveDirection;
        }
    }

    protected void SendAnimData() {
        m_npc.m_animator.SetFloat("yDirection", m_moveDirection.y);
        m_npc.m_animator.SetFloat("xDirection", m_moveDirection.x);
        m_npc.m_animator.SetFloat("lastYDirection", m_lastDirection.y);
        m_npc.m_animator.SetFloat("lastXDirection", m_lastDirection.x);
        m_npc.m_animator.SetFloat("xIdleDirection", m_directionVectors[(int)m_direction].x);
        m_npc.m_animator.SetFloat("yIdleDirection", m_directionVectors[(int)m_direction].y);
        m_npc.m_animator.SetBool("isMoving", m_npc.m_isMoving);
    }
}
