using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMovement : NpcMovement {

    [HideInInspector]
    public Mob m_mob;

    protected override void Start() {
        m_mob = gameObject.GetComponent<Mob>();
        m_npc = m_mob; // Seems dodgey. Look up a better way around this inheritance issue.

        m_mob.SetDirectionConstants();

        m_idleDirectionChangeTimer = 0f;
        m_idleDirectionWaitTime = Random.Range(1f, 5f);

        m_moveDeadZone = 0.3f;
    }

    protected override void Move() {
        m_mob.m_body.velocity = Vector2.zero;

        if (m_mob.M_followTarget != null) {

            if (m_mob.M_followTarget.m_directionVectors.Length > 0) {
                Vector2 vectorToTarget = ((Vector2)m_mob.M_followTarget.transform.position - m_mob.M_followTarget.m_directionVectors[(int)m_mob.M_followTarget.m_direction]) - (Vector2)m_mob.transform.position;

                if (Mathf.Abs(vectorToTarget.x) > 0.1f ||
                    Mathf.Abs(vectorToTarget.y) > 0.1f) {
                    m_mob.m_body.velocity = vectorToTarget.normalized * (m_mob.m_walkSpeed);
                }
            }
        }

        if (m_mob.m_isMoving && m_mob.M_followTarget != null) {
            m_lastDirection = m_mob.M_followTarget.m_movement.m_lastInputDirection;
        } else if(m_mob.m_isMoving) {
            m_lastDirection = m_moveDirection;
        }
    }
}
