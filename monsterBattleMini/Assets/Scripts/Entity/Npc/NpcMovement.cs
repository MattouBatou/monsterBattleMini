using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NpcMovement:MonoBehaviour {

    [HideInInspector]
    public Npc m_npc;
    [HideInInspector]
    public Entity m_followTarget;
    [HideInInspector]
    public Vector2 m_lastDirection;
    [HideInInspector]
    public Vector2 m_moveDirection;

    [HideInInspector]
    public float m_moveDeadZone;

    [HideInInspector]
    protected float m_idleDirectionChangeTimer;
    [HideInInspector]
    protected float m_idleDirectionWaitTime;

    protected virtual void Start () {

        m_npc = gameObject.GetComponent<Npc>();
        m_lastDirection = m_moveDirection = Vector2.zero;

        m_npc.SetDirectionConstants();

        m_idleDirectionChangeTimer = 0f;
        m_idleDirectionWaitTime = Random.Range(1f, 5f);

        m_moveDeadZone = 0.3f;
    }

    protected void FixedUpdate() {
        //Move();
        GetMove();
        SendAnimData();
    }

    protected void Update () {
        //SetMoveDirection();
        SetIdleFacingDirection();
	}


    //protected void SetMoveDirection() {
    //    m_moveDirection = Vector2.zero;

    //}

    protected void SetIdleFacingDirection() {
        if (!m_npc.m_isMoving && m_idleDirectionChangeTimer == 0f) {
            m_npc.m_direction = (Entity.Direction)Mathf.RoundToInt(Random.Range(0f, 3f));
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
            if (m_npc.m_body.velocity.x < -m_moveDeadZone) {
                m_npc.m_direction = Entity.Direction.left;
                m_moveDirection += m_npc.m_directionVectors[(int)m_npc.m_direction];
            }
            if (m_npc.m_body.velocity.x > m_moveDeadZone) {
                m_npc.m_direction = Entity.Direction.right;
                m_moveDirection += m_npc.m_directionVectors[(int)m_npc.m_direction];
            }
            if (m_npc.m_body.velocity.y < -m_moveDeadZone) {
                m_npc.m_direction = Entity.Direction.down;
                m_moveDirection += m_npc.m_directionVectors[(int)m_npc.m_direction];
            }
            if (m_npc.m_body.velocity.y > m_moveDeadZone) {
                m_npc.m_direction = Entity.Direction.up;
                m_moveDirection += m_npc.m_directionVectors[(int)m_npc.m_direction];
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

        // Add default movement logic for npc's here.

        if (m_npc.m_isMoving) {
            m_lastDirection = m_moveDirection;
        }
    }

    protected void SendAnimData() {
        m_npc.m_animator.SetFloat("yDirection", m_moveDirection.y);
        m_npc.m_animator.SetFloat("xDirection", m_moveDirection.x);
        m_npc.m_animator.SetFloat("lastYDirection", m_lastDirection.y);
        m_npc.m_animator.SetFloat("lastXDirection", m_lastDirection.x);
        m_npc.m_animator.SetFloat("xIdleDirection", m_npc.m_directionVectors[(int)m_npc.m_direction].x);
        m_npc.m_animator.SetFloat("yIdleDirection", m_npc.m_directionVectors[(int)m_npc.m_direction].y);
        m_npc.m_animator.SetBool("isMoving", m_npc.m_isMoving);
    }
}
