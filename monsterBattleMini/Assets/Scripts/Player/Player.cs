using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player:MonoBehaviour {

    [HideInInspector]
    public IInputManager m_input;
    [HideInInspector]
    public bool m_isMoving = false;
    [HideInInspector]
    public Rigidbody2D m_body;
    [HideInInspector]
    public GameObject m_attackBox; // TODO: Make attack box class
    [HideInInspector]
    public Animator m_animator;
    [HideInInspector]
    public float m_attackBox_time;

    public int m_playerId = 0;
    public float m_walkSpeed = 7f;

    private void Awake() {

        m_attackBox = new GameObject();
        m_attackBox.tag = "PlayerAttackBox";
        m_attackBox.name = gameObject.name + "_Attack_Box";
        AddBoxCollider2D(ref m_attackBox, new Vector2(0.5f, 0.5f), Vector2.zero, true);
        m_attackBox.transform.parent = gameObject.transform;
        m_attackBox.transform.localPosition = Vector3.zero;

        m_attackBox_time = 0.05f;
    }

    private void Start() {
        m_input = InputManager.instance;
        m_animator = gameObject.GetComponent<Animator>();
        m_body = gameObject.GetComponent<Rigidbody2D>();

        Physics2D.IgnoreCollision(m_body.GetComponent<BoxCollider2D>(), m_attackBox.GetComponent<BoxCollider2D>());
    }

    private void AddBoxCollider2D(ref GameObject go, Vector2 size, Vector2 offset, bool isTrigger) {
        BoxCollider2D boxCollider2D = go.AddComponent<BoxCollider2D>();
        boxCollider2D.size = size;
        boxCollider2D.offset = offset;
        boxCollider2D.isTrigger = isTrigger;
        boxCollider2D.enabled = false;
    }
}
