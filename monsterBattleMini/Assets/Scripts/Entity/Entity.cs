using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour {

    [HideInInspector]
    public bool m_isMoving = false;
    [HideInInspector]
    public Rigidbody2D m_body;
    [HideInInspector]
    public BoxCollider2D m_bodyBox;
    [HideInInspector]
    public GameObject m_hitbox;
    [HideInInspector]
    public Animator m_animator;
    [HideInInspector]
    public SpriteRenderer m_renderer;
    [HideInInspector]
    public enum Direction {
        up,
        down,
        left,
        right
    }

    [HideInInspector]
    public Vector2[] m_directionVectors;

    [HideInInspector]
    public Direction m_direction;

    public virtual void Start() {
        m_animator = gameObject.GetComponent<Animator>();
        m_body = gameObject.GetComponent<Rigidbody2D>();
        m_bodyBox = gameObject.GetComponent<BoxCollider2D>();
        m_renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void AddBoxCollider2D(ref GameObject go, string goName, string goTag, Vector2 size, Vector2 offset, bool isTrigger, bool enabled) {
        go = new GameObject(goName, typeof(BoxCollider2D));
        go.tag = goTag;
        BoxCollider2D boxCollider2D = go.GetComponent<BoxCollider2D>();
        boxCollider2D.size = size;
        boxCollider2D.offset = offset;
        boxCollider2D.isTrigger = isTrigger;
        boxCollider2D.enabled = enabled;

        go.transform.parent = gameObject.transform;
        go.transform.Translate(gameObject.transform.position);
    }

    public void AddBoxCollider2D(ref GameObject go, string goName, string goTag, Vector2 offset, bool isTrigger, bool enabled) {
        go = new GameObject(goName, typeof(BoxCollider2D));
        go.tag = goTag;
        BoxCollider2D boxCollider2D = go.GetComponent<BoxCollider2D>();
        boxCollider2D.offset = offset;
        boxCollider2D.isTrigger = isTrigger;
        boxCollider2D.enabled = enabled;

        go.transform.parent = gameObject.transform;
        go.transform.Translate(gameObject.transform.position);
    }

    public void SetDirectionConstants() {
        m_direction = Entity.Direction.down;

        m_directionVectors = new Vector2[4];

        m_directionVectors[(int)Entity.Direction.up] = Vector2.up;
        m_directionVectors[(int)Entity.Direction.down] = Vector2.down;
        m_directionVectors[(int)Entity.Direction.left] = Vector2.left;
        m_directionVectors[(int)Entity.Direction.right] = Vector2.right;
    }

    public void sortInWorld() {
        for (int i = 0; i < Camera.allCamerasCount; i++)
            m_renderer.sortingOrder = -(int)Camera.allCameras[i].WorldToScreenPoint(gameObject.transform.position).y;
    }

    public virtual void Update() {
        sortInWorld();
    }
}
