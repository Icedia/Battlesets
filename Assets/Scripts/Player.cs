using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IHealth<float>
{
    private float playerHealth;
    [SerializeField]private Slider playerHealthBar;

	void Start ()
    {
        Health(100);

	}
	
	void Update ()
    {
        Debug.Log(playerHealth);
        playerHealthBar.value = playerHealth;
        if (Input.GetKeyUp(KeyCode.T))
        {
            DoDamage(10);
        }

    }

    public void Health(float health)
    {
        playerHealth = health;
        Debug.Log(health);
    }

    private void DoDamage(int damage)
    {
        playerHealth -= damage;
    }
}
