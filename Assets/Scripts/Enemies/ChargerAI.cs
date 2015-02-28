//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34003
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using UnityEngine;
using System;

public class ChargerAI : MonoBehaviour {
	//Positions
	public Player player;
	private Vector2 speed, distance;

    //State checks
	private bool isAggroed;
    private bool isTired;
    private bool isCharging;

    //Components
	private Animator animator;
    //private MoveControllerNoAnimation moveControllerNoAnimation;
	
	System.Random rnd;
	private double t, timer;
	
	public void Start() {
  		//moveControllerNoAnimation = GetComponent<MoveControllerNoAnimation> ();
        animator = GetComponent<Animator>();

		distance = new Vector2 (0, 0);
		speed = new Vector2 (0, 0);

		t = 10;
		timer = 5;

		isAggroed = false;
        isTired = false;
        isCharging = false;
		rigidbody2D.mass = 5;
	}

	void Update() {
		rnd = new System.Random ();

        //Nir, this isnt necessary. You can get the position of the object the script is attached to by
        //transform.position because it assumes that it's its own transform if its not stated. thx bb delete this
		//enemyPosition = Enemy.position; WRONG
        //transform.position; woooo
        distance = player.transform.position - transform.position;
		

        //Check distance between the player and charger. If its close enough, aggro
		if (distance.magnitude < 5 && isTired == false) {
			isAggroed = true;
            isCharging = true;
            animator.SetBool("isCharging", true);
		}
		if (distance.magnitude > 5) {
			isAggroed = false;
		}
        speed = new Vector2(0, 0);

		if (isAggroed) {

            //Charge while the charge animation is playing
            if (isCharging) {
                speed = new Vector2(5 * distance.normalized.x, 5 * distance.normalized.y);
                rigidbody2D.velocity = speed;
            }
            //Dont move if charger has already charged and is now tired
            if (isTired) {
                speed = new Vector2(0, 0);
            }
		} //If the player isnt aggroed, it moves randomly
        else {
			if (t <= 0.9) {
				if(rigidbody2D.velocity.magnitude != 0) {
					speed = new Vector2 (0, 0);
					t = 2;
                }
			} else {
				int rand = rnd.Next (1, 5);
				if (rand == 1) {
					speed = new Vector2 (2,0);
					t = 1;
				} else if (rand == 2) {
					speed = new Vector2 (-2,0);
					t=1;
				} else if (rand == 3) {
					speed = new Vector2 (0,2);
					t=1;
				} else {
					speed = new Vector2 (0,-2);
					t=1;
				}
			}
		}
        Debug.Log(speed);
	    rigidbody2D.velocity = speed;
	}

    public void DoneCharging() {
        isTired = true;
        animator.SetBool("isCharging", false);
        animator.SetBool("isTired", true);
    }

    public void DoneResting() {
        isTired = false;
        animator.SetBool("isTired", false);
    }
}
	
