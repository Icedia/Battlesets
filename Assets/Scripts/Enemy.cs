using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IHealth<float>
{
    [SerializeField] private Slider enemyHealthBar;
    private float enemyHealth;

    [SerializeField] private Sprite enemyDefend;
    [SerializeField] private Sprite enemyAttack;

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

    public void DefendAnim()
    {
        print("Enemy defend!");
        GetComponent<SpriteRenderer>().sprite = enemyDefend;
    }

    public void AttackAnim()
    {
        print("Enemy attack!");
        GetComponent<SpriteRenderer>().sprite = enemyAttack;
    }

    public void Health(float health)
    {
        enemyHealth = health;
    }

    public void DoDamage(int damage)
    {
        enemyHealth -= damage;
    }
}
