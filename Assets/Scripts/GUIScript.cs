﻿using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {

    public Player player;
    public UILabel healthLabel;
    public UILabel scoreLabel;


    Health playerHealth;

	// Use this for initialization
	void Start () {
        playerHealth = player.GetComponent<Health>();
	}
	
	// Update is called once per frame
	void Update () {
        healthLabel.text = playerHealth.currentHealth.ToString();
        scoreLabel.text = GameManager.instance.points.ToString();
	}
}
