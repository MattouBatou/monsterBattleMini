﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player:Entity {

    [HideInInspector]
    public IInputManager m_input;
    [HideInInspector]
    public GameObject m_attackBox;
    [HideInInspector]
    public float m_attackBox_time;
    [HideInInspector]
    public PlayerMovement m_movement;
    [HideInInspector]
    public List<Mob> m_mobs;
    [HideInInspector]
    public int m_maxMobs = 6;


    public int m_playerId = 0;
    public float m_walkSpeed = 7f;

    private void Awake() {
        m_movement = gameObject.AddComponent<PlayerMovement>();

        AddBoxCollider2D(ref m_attackBox, gameObject.name + "_Attack_Box", "PlayerAttackBox", new Vector2(0.5f, 0.5f), Vector2.zero, true, false);
        AddBoxCollider2D(ref m_hitbox, gameObject.name + "_Hit_Box", "PlayerHitBox", new Vector2(0f, 0.57f), true, true);
        m_attackBox_time = 0.05f;

        m_mobs = new List<Mob> {
            Capacity = m_maxMobs
        };
    }

    public override void Start() {
        base.Start();
        m_input = InputManager.instance;

        Physics2D.IgnoreCollision(m_body.GetComponent<BoxCollider2D>(), m_attackBox.GetComponent<BoxCollider2D>());
    }

}
