using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Npc : Entity {

    [HideInInspector]
    public Rigidbody2D m_followTarget;

    public int m_npcId = 0;
    public float m_walkSpeed = 7f;

    protected virtual void Start () {
        m_isMoving = false;
        m_animator = gameObject.GetComponent<Animator>();
        m_body = gameObject.GetComponent<Rigidbody2D>();
    }
}
