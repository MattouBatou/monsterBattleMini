using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMovement : NpcMovement {
    protected override void Move() {
        m_npc.m_body.velocity = Vector2.zero;

        if (m_npc.m_followTarget != null) {

            if (m_npc.m_followTarget.m_directionVectors.Length > 0) {
                Vector2 vectorToTarget = ((Vector2)m_npc.m_followTarget.transform.position - m_npc.m_followTarget.m_directionVectors[(int)m_npc.m_followTarget.m_direction]) - (Vector2)m_npc.transform.position;

                m_npc.m_body.velocity = vectorToTarget.normalized * (m_npc.m_walkSpeed / 1.5f);

            }
        }

        if (m_npc.m_isMoving) {
            m_lastDirection = m_moveDirection;
        }
    }
}
