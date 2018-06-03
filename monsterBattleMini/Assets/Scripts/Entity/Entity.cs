using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour {

    [HideInInspector]
    public bool m_isMoving = false;
    [HideInInspector]
    public Rigidbody2D m_body;
    [HideInInspector]
    public GameObject m_hitbox;
    [HideInInspector]
    public Animator m_animator;

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
}
