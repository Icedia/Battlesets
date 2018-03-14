﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IHealth<float>
{
    [SerializeField] private Slider enemyHealthBar;
    private float enemyHealth;

    void Start ()
    {
        Health(100); // set health for the player
	}
	
	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Y)) // replace with a other way to do damage
        {
            DoDamage(10);
        }
        enemyHealthBar.value = enemyHealth;
	}

    public void Health(float health)
    {
        enemyHealth = health;
    }

    private void DoDamage(int damage)
    {
        enemyHealth -= damage;
    }
}
