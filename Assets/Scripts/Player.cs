using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IHealth<float>
{
    private float playerHealth;

	void Start ()
    {
        Health(100);

	}
	
	void Update ()
    {
        Debug.Log(playerHealth);
	}

    public void Health(float health)
    {
        playerHealth = health;
        Debug.Log(health);
    }
}
