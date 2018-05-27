using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private IInputManager input;
    private PlayerMovement m_player;
    private GameObject player;
    private int m_playerId = -1;
    public BoxCollider2D boxCollider2D;

	void Start () {
        input = InputManager.instance;

        m_player = (PlayerMovement)gameObject.GetComponent("PlayerMovement");

        if (m_player != null) {
            if (m_player.playerId >= 0) {
                m_playerId = m_player.playerId;
            }
        }

        player = gameObject;

        AddBoxCollider2D(ref player, Vector2.one, Vector2.zero);
    }


	void Update () {
        GetInput();
    }

    private void GetInput() {
        if (input.GetButton(m_playerId, InputAction.Attack)) {
            // Do something
        }
    }

    private void AddBoxCollider2D(ref GameObject player, Vector2 size, Vector2 offset) {
        BoxCollider2D boxCollider2D = player.AddComponent<BoxCollider2D>();
        boxCollider2D.size = size;
        boxCollider2D.offset = offset;
    }


}
