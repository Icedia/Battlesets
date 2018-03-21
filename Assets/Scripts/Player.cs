using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IHealth<float>
{
    [SerializeField] private float playerHealth;
    [SerializeField] private Slider playerHealthBar;

    // Spriterenderer 
    [SerializeField] private SpriteRenderer spriteRenderer;

    // Sprites
    [SerializeField] private Sprite playerIdle;
    [SerializeField] private Sprite playerHurt;
    [SerializeField] private Sprite playerAttack;

	void Start ()
    {
        Health(100);
	}

    public void IdleAnim()
    {
        spriteRenderer.sprite = playerIdle;
    }

    public void HurtAnim()
    {
        spriteRenderer.sprite = playerHurt;
    }

    public void AttackAnim()
    {
        spriteRenderer.sprite = playerAttack;
    }

    public void Health(float health)
    {
        playerHealth = health;
    }

    public void DoDamage(int damage)
    {
        playerHealth -= damage;
        playerHealthBar.value = playerHealth;
    }
}
