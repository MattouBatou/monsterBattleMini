using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement:MonoBehaviour {

    private IInputManager input;
    public int playerId = 0;

    Vector2 moveInputDirection;
    Vector2 lastInputDirection;
    bool isMoving = false;

    private Animator _Anim;

    public float walkSpeed = 7f;

    private void Start() {
        input = InputManager.instance;
        _Anim = this.GetComponent<Animator>();

        lastInputDirection = Vector2.zero;
    }

    private void Update() {
        GetInput();
        Move();
        SendAnimData();
    }

    public void Move() {
        isMoving = false;

        if (moveInputDirection.x != 0 || moveInputDirection.y != 0) {
            isMoving = true;

            lastInputDirection.x = moveInputDirection.x;
            lastInputDirection.y = moveInputDirection.y;

            transform.Translate(moveInputDirection * walkSpeed * Time.deltaTime);
        }
    }

    public void GetInput() {
        moveInputDirection = Vector2.zero;

        // TODO: Get player id dynamically for multiplayer input.GetButtonDown(playerId, InputAction)
        if (input.GetButton(playerId, InputAction.Up)) { moveInputDirection += Vector2.up; }
        if (input.GetButton(playerId, InputAction.Down)) { moveInputDirection += Vector2.down; }
        if (input.GetButton(playerId, InputAction.Left)) { moveInputDirection += Vector2.left; }
        if (input.GetButton(playerId, InputAction.Right)) { moveInputDirection += Vector2.right; }

        moveInputDirection.Normalize();
    }

    /// <summary>
    /// This is the function where we will be sending information to the animator
    /// </summary>
    private void SendAnimData() {
        _Anim.SetFloat("yDirection", moveInputDirection.y);
        _Anim.SetFloat("xDirection", moveInputDirection.x);
        _Anim.SetFloat("lastYDirection", lastInputDirection.y);
        _Anim.SetFloat("lastXDirection", lastInputDirection.x);
        _Anim.SetBool("isMoving", isMoving);
    }

}
