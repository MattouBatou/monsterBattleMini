using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour {

    [HideInInspector]
    public bool m_isMoving = false;
    [HideInInspector]
    public Rigidbody2D m_body;
    [HideInInspector]
    public Animator m_animator;

    public int m_npcId = 0;
    public float m_walkSpeed = 7f;

	private void Start () {
        m_animator = gameObject.GetComponent<Animator>();
        m_body = gameObject.GetComponent<Rigidbody2D>();
    }
}
