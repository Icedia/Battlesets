using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IHealth<float>
{
    private float playerHealth;
    [SerializeField]private Slider playerHealthBar;

    [SerializeField] private Sprite playerDefend;
    [SerializeField] private Sprite playerAttack;

	void Start ()
    {
        Health(100);
	}
	
	void Update ()
    {
        playerHealthBar.value = playerHealth;
        if (Input.GetKeyUp(KeyCode.T))
        {
            DoDamage(10);
        }

    }

    public void DefendAnim()
    {
        GetComponent<SpriteRenderer>().sprite = playerDefend;
    }

    public void AttackAnim()
    {
        GetComponent<SpriteRenderer>().sprite = playerAttack;
    }

    public void Health(float health)
    {
        playerHealth = health;
    }

    public void DoDamage(int damage)
    {
        playerHealth -= damage;
    }
}
