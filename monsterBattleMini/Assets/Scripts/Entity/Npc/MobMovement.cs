using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMovement : NpcMovement {

    [HideInInspector]
    public Mob m_mob;
    [HideInInspector]
    public readonly Vector2 k_mobOffset = new Vector2(0.5f, 0.5f);

    Vector3[] path;
    int targetIndex;

    protected override void Start() {
        m_mob = gameObject.GetComponent<Mob>();
        m_npc = m_mob; // Seems dodgey. Look up a better way around this inheritance issue.

        m_mob.SetDirectionConstants();

        m_idleDirectionChangeTimer = 0f;
        m_idleDirectionWaitTime = Random.Range(1f, 5f);

        m_moveDeadZone = 0.3f;

        if (!m_mob.M_followTarget) {
            PathRequestManager.RequestPath(m_mob.transform.position, PathRequestManager.GetRandomNode().worldPosition, OnPathFound);
        }
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
        if (pathSuccessful) {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath() {
        Vector3 currentWaypoint = path[0];

        while (true) {
            if(m_mob.transform.position == currentWaypoint) {
                targetIndex++;
                if(targetIndex >= path.Length) {
                    yield return new WaitForSeconds(3f);
                    targetIndex = 0;
                    PathRequestManager.RequestPath(m_mob.transform.position, PathRequestManager.GetRandomNode().worldPosition, OnPathFound);
                    yield break;
                }

                currentWaypoint = path[targetIndex];
            }

            //m_mob.m_body.velocity = Vector2.zero;

            //Vector2 vectorToTarget = (
            //    (Vector2)currentWaypoint + (k_mobOffset * m_lastDirection)) -
            //    (Vector2)m_mob.transform.position;

            //if (Mathf.Abs(vectorToTarget.x) > 0.1f ||
            //        Mathf.Abs(vectorToTarget.y) > 0.1f) {
            //    m_mob.m_body.velocity = vectorToTarget.normalized * (m_mob.m_walkSpeed);
            //}

            m_mob.transform.position = Vector3.MoveTowards(m_mob.transform.position, currentWaypoint, m_mob.m_walkSpeed * Time.deltaTime);
            yield return null;
        }
    }

    protected override void Move() {
        m_mob.m_body.velocity = Vector2.zero;

        if (m_mob.M_followTarget != null) {
            int mobIndex = m_mob.M_followTarget.m_mobs.FindIndex(x => { return x.m_npcId == m_mob.m_npcId; });

            if (m_mob.M_followTarget.m_directionVectors.Length > 0) {
                // Position behind follow target with a given amount of space (k_mobOffset).

                Vector2 vectorToTarget = (
                    (Vector2)m_mob.M_followTarget.transform.position -
                    ( ( m_mob.M_followTarget.m_directionVectors[(int)m_mob.M_followTarget.m_direction]
                        + (k_mobOffset * m_lastDirection)) * (mobIndex + 1) ) ) -
                        (Vector2)m_mob.transform.position;

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

    public void OnDrawGizmos() {
        if(path != null) {
            for(int i = targetIndex; i < path.Length; i++) {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one * 0.5f);

                if(i == targetIndex) {
                    Gizmos.DrawLine(m_mob.transform.position, path[i]);
                } else {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}
