﻿using UnityEngine;
using System.Collections;

public class MoveController : MonoBehaviour {

    // Components
    private Animator animator;
    private Transform transform;
    // Speed of object
    [Range(0, 10)]
    public float speed = 8;
    public float dashSpeed = 100;
    // Direction of object
    internal Vector2 direction;
    internal Vector2 facing;
    // Actual movement
    internal Vector2 movementVector = new Vector2(0, 0);
    private bool isMoving;
    private bool isDashing;
    private bool canDash;
    public float dashIn;

    void Awake() {
        animator = GetComponent<Animator>();
        transform = GetComponent<Transform>();
        isMoving = false;
        movementVector = new Vector2(0, 0);
        direction = new Vector2(0, 0);
        facing = new Vector2(0, 0);
        canDash = true;
        isDashing = false;
        dashIn = 0;
    }

	// Update is called once per frame
	void Update () {
        //Subtract the cooldown for dashing and check when the player can dash again and whether or not its finished dashing
        dashIn -= Time.deltaTime;
        if (dashIn < 0.8) {
            isDashing = false;
            rigidbody2D.drag = 75;
            rigidbody2D.mass = 2;
        }
        if (dashIn < 0.2)  canDash = true;


        /* Check if character is moving and store the direction the player is facing in case they stop moving*/
        if (rigidbody2D.velocity.normalized.x != 0 || rigidbody2D.velocity.normalized.y != 0) {
            facing = rigidbody2D.velocity.normalized;
            isMoving = true;
        }
        if (movementVector.x == 0 && movementVector.y == 0 && isDashing == false) {
            isMoving = false;
        }

        //Check whether sprite is facing left or right. Flip the sprite based on its direction
        if (facing.x > 0) {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Calculate movement amount
        movementVector = direction * speed;

        //Play animations
        if (isMoving == true) {
            animator.SetBool("IsMoving", isMoving);
            animator.SetFloat("movement_x", movementVector.x);
            animator.SetFloat("movement_y", movementVector.y);
            animator.SetFloat("facing_x", facing.x);
            animator.SetFloat("facing_y", facing.y);
        }
        else {
            animator.SetBool("IsMoving", false);
        }
        if (isDashing) {
            animator.SetBool("IsDashing", true);
        }
        else {
            animator.SetBool("IsDashing", false);
        } 
    }
    void FixedUpdate() {
        // Apply the movement to the rigidbody
        //rigidbody2D.AddForce(movementVector);
        if (isDashing == false) {
            rigidbody2D.velocity = movementVector;
        }
            

        //Debug.Log("speed:" + speed + "direction:" + direction + "movementVector" + movementVector);
    }

    /* Moves the object in a direction by a small amount (used for player input) */
    internal void Move(float input_X, float input_Y) {
        direction = new Vector2(input_X, input_Y);
    }

    /* Moves the object towards a destination by a small amount (used for enemy input)*/
    internal void Move(Vector2 _direction) {
        direction = _direction;
    }

    /*pushes a character in a direction by an amount*/
    internal void Push(Vector2 direction, float amount) {
        rigidbody2D.AddForce(direction * amount);
    }

    public void Dash() {
        // Debug.Log("Facing:" + facing);
        // Only let the player dash if the cooldown is < 0. If he can, dash and reset the timer       
        if (canDash) {
            rigidbody2D.mass = 1;
            rigidbody2D.drag = 33;
            //rigidbody2D.AddForce(facing * dashSpeed);
            rigidbody2D.velocity = facing * dashSpeed;
            dashIn = 1;
            isMoving = true;
            isDashing = true;
            canDash = false;
        }
    }
}