using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour {

    public int m_health = 2;

    private float shakeAmount;
    private float shakeSpeed;
    private bool canShake = false;
    private float shakeOffset;
    private float shakeTime = 0.3f;
    private float shakeTimer = 0.0f;
    private Vector2 posBeforeShake;
    private Vector2 collisionDirectionDiff;
    [HideInInspector]
    public SpriteRenderer m_renderer;

    private enum direction {
        top,
        bottom,
        left,
        right,
        none
    };

    private direction collisionDirection;

    private void Start() {
        shakeAmount = 0.02f;
        shakeSpeed = 75f;

        collisionDirectionDiff = Vector2.zero;
        collisionDirection = direction.none;
        m_renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update() {
        if (canShake) {
            shake();
        }

        if (m_health <= 0 && !canShake) {
            breakObject();
        }

        sortInWorld();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        switch (other.tag) {
            case "PlayerAttackBox":
                reduceHealth(-1);
                // TODO: Make PlayerAttackBox a class that manages disabling itself when colliding with subscribed colliders via tags.
                other.enabled = false;

                collisionDirectionDiff.x = transform.position.x - other.transform.position.x;
                collisionDirectionDiff.y = transform.position.y - other.transform.position.y;

                checkCollisionDirection();

                break;
        }
    }

    private void breakObject() {
        GameObject.Destroy(gameObject);
    }

    private void reduceHealth(int damageVal) {
        m_health += damageVal;

        canShake = true;
        posBeforeShake = transform.position;
    }

    private void shake() {
        /// TODO: Dynamically add sprite prefab to box component prefab and animate sprite transform instead to not animate the collider.
        Vector3 position = transform.position;
        shakeOffset = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;

        if(collisionDirection == direction.top) {
            position.y += shakeOffset;
        } else if (collisionDirection == direction.bottom) {
            position.y -= shakeOffset;
        } else if (collisionDirection == direction.left) {
            position.x += shakeOffset;
        } else if (collisionDirection == direction.right) {
            position.x -= shakeOffset;
        }

        transform.position = position;

        if(shakeTimer >= shakeTime) {
            canShake = false;
            shakeTimer = 0.0f;
            transform.position = posBeforeShake;
        }

        shakeTimer += Time.deltaTime;
    }

    private void checkCollisionDirection() {
        // TODO: Try passing entities facing direction and animate in the opposite direction.

        if (collisionDirectionDiff.x < 0 && collisionDirectionDiff.y < 0) {

            if (Mathf.Abs(collisionDirectionDiff.x) > Mathf.Abs(collisionDirectionDiff.y)) {
                collisionDirection = direction.right;
            } else {
                collisionDirection = direction.top;
            }

        } else if (collisionDirectionDiff.x > 0 && collisionDirectionDiff.y < 0) {

            if (Mathf.Abs(collisionDirectionDiff.x) > Mathf.Abs(collisionDirectionDiff.y)) {
                collisionDirection = direction.left;
            } else {
                collisionDirection = direction.top;
            }

        } else if (collisionDirectionDiff.x < 0 && collisionDirectionDiff.y > 0) {

            if (Mathf.Abs(collisionDirectionDiff.x) > Mathf.Abs(collisionDirectionDiff.y)) {
                collisionDirection = direction.right;
            } else {
                collisionDirection = direction.bottom;
            }

        } else if (collisionDirectionDiff.x > 0 && collisionDirectionDiff.y > 0) {

            if (Mathf.Abs(collisionDirectionDiff.x) > Mathf.Abs(collisionDirectionDiff.y)) {
                collisionDirection = direction.left;
            } else {
                collisionDirection = direction.bottom;
            }

        } else {

            collisionDirection = direction.none;

        }
    }

    public void sortInWorld() {
        for (int i = 0; i < Camera.allCamerasCount; i++)
            m_renderer.sortingOrder = -(int)Camera.allCameras[i].WorldToScreenPoint(gameObject.transform.position).y;
    }
}
